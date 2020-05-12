using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCRM.Options;

namespace TinyCRM
{
    public class OrderService : IOrderService
    {
        private TinyCrmDbContext dbContext;
        private ICustomerService customerService;
        private IProductService productService;
        public OrderService(ICustomerService cService, TinyCrmDbContext context)
        {
            dbContext = context;
            customerService = cService;
            productService = new ProductService(dbContext);
        }
        public Order CreateOrder(CreateOrderOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var customer = customerService.SearchCustomer(new CustomerSearchOptions()
            {
                CustomerId = options.CustomerId
            }).Include(c=>c.OrderList)
            .ThenInclude(o=>o.OrderProducts)
            .SingleOrDefault();

            if (customer == null)
            {
                return null;
            }

            var order = new Order();
            foreach (var id in options.ProductIds)
            {
                var product = productService.SearchProduct(new ProductSearchOptions() { 
                    ProductId = id 
                }).SingleOrDefault();

                if (product==null)
                {
                    return null;
                }

                order.OrderProducts.Add(new OrderProduct() { ProductId = product.ProductId });
                order.TotalAmount = order.TotalAmount + product.Price;
            }
            if (options.DeliveryAddress != null)
            {
                order.DeliveryAddress = options.DeliveryAddress;
            }
            customer.OrderList.Add(order);
            //dbContext.Update(customer);//??

            if (dbContext.SaveChanges() > 0)
            {
                return order;
            }

            return null;

        }

        public Order UpdateOrder(UpdateOrderOptions options)
        {
            if (options == null || options.OrderId == null)
            {
                return null;
            }
            var order = dbContext
                .Set<Order>()
                .Where(x => x.OrderId == options.OrderId)
                .Include(o => o.OrderProducts)
                .SingleOrDefault();

            if (order == null)
            {
                return null;
            }

            if (options.DeliveryAddress != null)
            {
                order.DeliveryAddress = options.DeliveryAddress;
            }

            if (options.ProductIds.Count!=0)
            {
                order.OrderProducts.Clear();

                order.TotalAmount = 0M;

                foreach (var id in options.ProductIds)
                {
                    var product = productService.GetProductById(id);

                    if (product == null)
                    {
                        return null;
                    }

                    order.OrderProducts.Add(new OrderProduct()
                    {
                        Product = product
                    });

                    order.TotalAmount += product.Price;
                }
            }
            
            if (dbContext.SaveChanges() > 0)
            {
                return order;
            }
            return null;
        }

        public IQueryable<Order> SearchOrder(OrderSearchOptions options)
        {
            if (options == null)
            {
                //throw new ArgumentNullException("Null Options");
                return null;
            }

            var query = dbContext
                .Set<Order>()
                .AsQueryable();

            if (options.OrderId != null)
            {
                query = query.Where(o => o.OrderId==options.OrderId);
            }

            if (options.CustomerId != null)
            {
                query = query.Where(o => o.CustomerId == options.CustomerId.Value);
            }

            query = query.Take(500);
            return query;

        }
    }
}

