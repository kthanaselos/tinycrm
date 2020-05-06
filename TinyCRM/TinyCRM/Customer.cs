using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TinyCRM
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public DateTime Created { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string VatNumber { get; set; }
        public string Phone { get; set; }
        public decimal TotalGross { get { return CalculateTotalGross(); } }
        public bool IsActive { get; set; }
        public List<Order> OrderList { get; set; }


        //public Customer(string vat)
        //{
        //    if (!IsValidVat(vat))
        //    {
        //        throw new Exception("Invalid VatNumber");
        //    }

        //    VatNumber = vat;
        //    Created = DateTime.Now;
        //    OrderList = new List<Order>();
        //}

        private decimal CalculateTotalGross()
        {
            decimal sum = 0;
            foreach (var order in OrderList)
            {
                foreach (var p in order.ProductList)
                {
                    sum = sum + p.Price;
                }
            }
            return sum;
        }

        public bool IsHighValuedCustomer()
        {
            return TotalGross >= 10000M;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;
        }

        public bool IsValidVat(string vatnumber)
        {
            if (string.IsNullOrWhiteSpace(vatnumber))
            {
                return false;
            }

            vatnumber = vatnumber.Trim();

            if (int.TryParse(vatnumber, out int result) && vatnumber.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }
            Email = Email.Trim();

            if (Email.Contains("@") && Email.EndsWith(".com"))
            {
                return true;
            }

            return false;
        }
    }
}
