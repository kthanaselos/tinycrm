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
        private ICustomerService customerService;
        public CustomerController(ICustomerService cService)
        {
            customerService = cService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerOptions options)
        {
            var result = customerService.CreateCustomer(options);

            if (!result.Success){
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Json(result.Data);
        }


        [HttpGet("index")]
        public IActionResult Index()
        {
            var customerList = customerService
                .SearchCustomer(new CustomerSearchOptions())
                .ToList();

            return View(customerList);
        }

        [HttpGet("{id}/edit")]
        public IActionResult Edit(int? id)
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

            return View(customer);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int? id)
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

            return View(customer);
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id,[FromBody] CustomerUpdateOptions options)
        {
            var result = customerService.UpdateCustomer(options,id);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var result = customerService.DeleteCustomerById(id);

            if (!result.Success)
            {
                return StatusCode((int)result.ErrorCode, result.ErrorText);
            }

            return Ok();
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
    }
}