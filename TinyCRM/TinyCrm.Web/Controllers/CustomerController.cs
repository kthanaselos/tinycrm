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
    [Route("customer")]
    public class CustomerController : Controller
    {
        private TinyCrmDbContext dbContext;
        private ICustomerService customerService;
        public CustomerController()
        {
            dbContext = new TinyCrmDbContext();
            customerService = new CustomerService(dbContext);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerOptions options)
        {
            var customer = customerService.CreateCustomer(options);

            if (customer == null)
            {
                return BadRequest();
            }

            return Json(customer);
        }


        [HttpGet]
        public IActionResult Index()
        {
            var customerList = customerService
                .SearchCustomer(new CustomerSearchOptions())
                .ToList();

            return Json(customerList);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id,[FromBody] CustomerUpdateOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            var customer = customerService.UpdateCustomer(options,id);

            return Json(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = customerService.DeleteCustomerById(id);
            if (result == false)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult Search(CustomerSearchOptions options)
        {
            if (options == null)
            {
                return BadRequest();
            }

            var customers = customerService
                .SearchCustomer(options)
                .ToList();

            if (customers == null)
            {
                return NotFound();
            }

            return Json(customers);
        }

        [HttpGet("{id}")]
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