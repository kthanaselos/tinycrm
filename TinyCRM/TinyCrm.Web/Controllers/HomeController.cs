using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;
using TinyCrm.Web.Models;

namespace TinyCrm.Web.Controllers
{
    public class HomeController : Controller
    {
        private TinyCrmDbContext dbContext;
        private ICustomerService customerService;
        private IProductService productService;

        public HomeController(ICustomerService cService,IProductService pService,TinyCrmDbContext context)
        {
            customerService = cService;
            productService = pService;
            dbContext = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();

            model.Customers = customerService.SearchCustomer(
                new CustomerSearchOptions())
                .ToList(); 

            model.Products = productService.SearchProduct(new ProductSearchOptions())
                .ToList();
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult AddProduct()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
