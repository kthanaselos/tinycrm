using System;
using System.Linq;
using System.Net.Sockets;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private TinyCrmDbContext dbContext;
        public CustomerService(TinyCrmDbContext context)
        {
            dbContext= context;
        }
        public Result<Customer> CreateCustomer(CreateCustomerOptions options)
        {
            if (options == null)
            {
                return Result<Customer>
                    .CreateFailed(StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Vatnumber))
            {

                return Result<Customer>
                    .CreateFailed(StatusCode.BadRequest, "Null or empty Vatnumber");
                
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

            try
            {
                if (dbContext.SaveChanges() > 0)
                {
                    return Result<Customer>
                    .CreateSuccessful(customer);
                }
                else
                {
                    return Result<Customer>
                    .CreateFailed(StatusCode.InternalServerError, "Customer could not be updated");

                }

            }
            catch(Exception ex)
            {
                return Result<Customer>
                    .CreateFailed(StatusCode.InternalServerError, ex.ToString());
            }
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

        public Result<bool> UpdateCustomer (CustomerUpdateOptions options,int id)
        {
            var result = new Result<bool>();
            
            if (options == null)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "Null options";
                return result;
            }

            var customer = GetCustomerById(id);
            if (customer == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Customer with id {id} was not found";
                return result;
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
                customer.Phone = options.Phone;
            }
            if (options.IsActive != null)
            {
                customer.IsActive = options.IsActive.Value;
            }

            if (dbContext.SaveChanges() > 0)
            {
                result.ErrorCode = StatusCode.OK;
                result.Data = true;
                return result;
            }

            result.ErrorCode = StatusCode.InternalServerError;
            result.ErrorText = $"Customer could not be updated";
            return result;

        }

        public Result<bool> DeleteCustomerById(int id)
        {
            var result = new Result<bool>();

            if (id <= 0)
            {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = $"Id {id} is invalid";
                return result;
            }

            var customer = GetCustomerById(id);

            if (customer == null)
            {
                result.ErrorCode = StatusCode.NotFound;
                result.ErrorText = $"Customer with id {id} was not found";
                return result;
            }

            try
            {
                dbContext.Remove(customer);
                if (dbContext.SaveChanges() > 0)
                {
                    result.ErrorCode = StatusCode.OK;
                    result.Data = true;
                    return result;
                }
            }catch (Exception ex)
            {
                result.ErrorCode = StatusCode.InternalServerError;
                result.ErrorText = ex.ToString();
                return result;
            }

            result.ErrorCode = StatusCode.InternalServerError;
            result.ErrorText = $"Customer could not be deleted";
            return result;

        }
    }
}
