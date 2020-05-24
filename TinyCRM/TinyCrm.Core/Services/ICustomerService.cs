using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        IQueryable<Customer> SearchCustomer(CustomerSearchOptions options);

        Result<Customer> CreateCustomer(CreateCustomerOptions options);

        Customer GetCustomerById(int id);

        Result<bool> UpdateCustomer(CustomerUpdateOptions options, int id);

        Result<bool> DeleteCustomerById(int id);
    }
}
