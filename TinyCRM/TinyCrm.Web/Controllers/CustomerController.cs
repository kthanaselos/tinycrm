using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
        private TinyCrmDbContext dbContext;
        private ICustomerService customerService;
        public CustomerController()
        {
            dbContext = new TinyCrmDbContext();
            customerService = new CustomerService(dbContext);
        }

        public IActionResult Index()
        {
            var customerList = customerService
                .SearchCustomer(new CustomerSearchOptions())
                .ToList();

            return Json(customerList);
        }

        public IActionResult Search()
        {
            //todo
        }

        public IActionResult GetByiD(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var customer = customerService.SearchCustomer(new CustomerSearchOptions()
            {
                CustomerId = id
            }).SingleOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            return Json(customer);
        }
    }
}