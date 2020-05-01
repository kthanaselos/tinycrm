using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    public class Order
    {
        public string OrderId { get; private set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; private set; }
        public List<Product> ProductList { get; set; }

        public Order()
        {
            ProductList = new List<Product>();
        }
    }
}
