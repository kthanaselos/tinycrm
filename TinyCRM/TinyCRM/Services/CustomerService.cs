using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyCRM
{
    public class CustomerService : ICustomerService
    {
        private TinyCrmDbContext dbContext;
        public CustomerService(TinyCrmDbContext context)
        {
            dbContext= context;
        }
        public Customer CreateCustomer(CreateCustomerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var customer = new Customer()
            {
                Firstname = options.Firstname,
                Lastname = options.Lastname,
                Email = options.Email,
                VatNumber = options.Vatnumber,
                Phone = options.Phone,
                IsActive = true
            };

            dbContext.Add(customer);

            if (dbContext.SaveChanges() > 0)
            {
                return customer;
            }

            return null;

        }

        public IQueryable<Customer> SearchCustomer(CustomerSearchOptions options)
        {
            if (options == null)
            {
                //throw new ArgumentNullException("Null Options");
                return null;
            }

            var query = dbContext
                .Set<Customer>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.FirstName))
            {
                query = query.Where(c => c.Firstname == options.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(options.LastName))
            {
                query = query.Where(c => c.Lastname == options.LastName);
            }

            if (!string.IsNullOrWhiteSpace(options.VatNumber))
            {
                query = query.Where(c => c.VatNumber == options.VatNumber);
            }

            if (options.CreatedFrom != default(DateTime))
            {
                query = query.Where(c => c.Created >= options.CreatedFrom);
            }

            if (options.CreatedTo != default(DateTime))
            {
                query = query.Where(c => c.Created <= options.CreatedTo);
            }

            if (options.CustomerId != null)
            {
                query = query.Where(c => c.CustomerId == options.CustomerId.Value);
            }

            query = query.Take(500);
            return query;

        }

        public Customer GetCustomerById(int id)
        {
            var customer= SearchCustomer(new CustomerSearchOptions() { CustomerId = id }).SingleOrDefault();
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        public Customer UpdateCustomer (CustomerUpdateOptions options,Customer customer) //orisma Customer h kalytera CustomerId???
        {
            if (options == null || customer==null)
            {
                return null;
            }
            if (options.Firstname != null)
            {
                customer.Firstname = options.Firstname;
            }
            if (options.Lastname != null)
            {
                customer.Lastname = options.Lastname;
            }
            if (options.Email != null)
            {
                customer.Email = options.Email;
            }
            if (options.Phone != null)
            {
                customer.Phone = options.Email;
            }
            if (options.IsActive != null)
            {
                customer.IsActive = options.IsActive.Value;
            }

            if (dbContext.SaveChanges() > 0)
            {
                return customer;
            }

            return null;

        }
    }
}
