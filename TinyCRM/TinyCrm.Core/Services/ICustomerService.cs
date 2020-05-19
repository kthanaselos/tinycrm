using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        IQueryable<Customer> SearchCustomer(CustomerSearchOptions options);

        Customer CreateCustomer(CreateCustomerOptions options);

        Customer GetCustomerById(int id);

        Customer UpdateCustomer(CustomerUpdateOptions options, int id);

        public bool DeleteCustomerById(int id);
    }
}
