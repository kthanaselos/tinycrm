using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Web.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private IProductService productService; 
        public ProductController(IProductService pService)
        {
            productService = pService;
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View(productService.SearchProduct(new ProductSearchOptions()).ToList());
        }

        [HttpPost]
        public Product AddProduct ([FromBody] CreateProductOptions options)
        {
            return productService.CreateProduct(options);
        }
    }
}