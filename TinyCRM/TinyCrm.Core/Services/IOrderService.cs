using System.Linq;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services.Options;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        public IQueryable<Order> SearchOrder(OrderSearchOptions options);
        Order CreateOrder(CreateOrderOptions options);

        public Order UpdateOrder(UpdateOrderOptions options);
    }
}
