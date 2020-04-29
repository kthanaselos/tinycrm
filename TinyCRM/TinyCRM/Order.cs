using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    class Order
    {
        public string OrderId { get; private set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; private set; }
    }
}
