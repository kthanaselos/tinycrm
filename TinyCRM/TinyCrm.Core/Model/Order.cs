using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        public int OrderId { get; private set; }
        public DateTimeOffset Created { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {
            Created = DateTimeOffset.Now;
            OrderProducts = new List<OrderProduct>();
        }
    }
}
