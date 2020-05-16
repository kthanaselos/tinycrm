using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
        Product CreateProduct(CreateProductOptions options);
        IQueryable<Product> SearchProduct(ProductSearchOptions options);
        Product GetProductById(string id);
        bool UpdateProduct(ProductUpdateOptions options, string productId);
    }
}
