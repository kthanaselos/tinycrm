using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCRM.Options;

namespace TinyCRM
{
    public interface IProductService
    {
        Product CreateProduct(CreateProductOptions options);
        IQueryable<Product> SearchProduct(ProductSearchOptions options);
        Product GetProductById(string id);
        bool UpdateProduct(ProductUpdateOptions options, string productId);
    }
}
