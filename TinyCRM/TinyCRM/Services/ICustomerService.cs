using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyCRM
{
    public interface ICustomerService
    {
        IQueryable<Customer> SearchCustomer(CustomerSearchOptions options);

        Customer CreateCustomer(CreateCustomerOptions options);

        Customer GetCustomerById(int id);

        Customer UpdateCustomer(CustomerUpdateOptions options, Customer customer);
    }
}
