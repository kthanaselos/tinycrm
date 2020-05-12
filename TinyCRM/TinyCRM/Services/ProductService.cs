using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using TinyCRM.Options;

namespace TinyCRM
{
    public class ProductService : IProductService
    {
        TinyCrmDbContext dbContext;

        public ProductService(TinyCrmDbContext context)
        {
            dbContext = context;
        }

        public Product CreateProduct (CreateProductOptions options)
        {
            if (options == null)
            {
                return null;
            }

            if (SearchProduct(new ProductSearchOptions() { 
                ProductId=options.ProductId
            }).Any())
            {
                return null;
            }

            var product = new Product()
            {
                ProductId = options.ProductId,
                Name = options.Name,
                Description=options.Description,
                Category=options.Category,
                Price=options.Price
            };

            dbContext.Add(product);

            if (dbContext.SaveChanges()>0)
            {
                return product;
            }

            return null;
        }
        public IQueryable<Product> SearchProduct(ProductSearchOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = dbContext
                     .Set<Product>()
                     .AsQueryable();

            if (options.ProductId != null)
            {
                query = query.Where(p => p.ProductId == options.ProductId);
            }

            if (options.PriceFrom != null)
            {
                query = query.Where(p => p.Price >= options.PriceFrom);
            }

            if (options.PriceTo != null)
            {
                query = query.Where(p => p.Price <= options.PriceTo);
            }

            if (options.Categories != null)
            {

            }

            query = query.Take(500);
            return query;


        }

        public Product GetProductById(string id)
        {
            var product= SearchProduct(new ProductSearchOptions()
            {
                ProductId = id
            }).SingleOrDefault();
            if (product == null)
            {
                return null;
            }
            return product;
        }

        public bool UpdateProduct(ProductUpdateOptions options, string productId)
        {
            if (options == null)
            {
                return false;
            }

            var product = SearchProduct(new ProductSearchOptions()
            {
                ProductId = productId
            }).SingleOrDefault();

            if (product == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                product.Name = options.Name;
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                product.Description = options.Description;
            }

            if (options.Price != null)
            {
                product.Price = options.Price.Value;
            }

            if (options.Category != null)
            {
                product.Category = options.Category.Value;
            }

            if (dbContext.SaveChanges()>0)
            {
                return true;
            }
            return false;
        }
    }
}
