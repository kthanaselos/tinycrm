using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCRM.Options;

namespace TinyCRM
{
    public interface IOrderService
    {
        public IQueryable<Order> SearchOrder(OrderSearchOptions options);
        Order CreateOrder(CreateOrderOptions options);

        public Order UpdateOrder(UpdateOrderOptions options);
    }
}
