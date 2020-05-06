using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    public class CustomerSearchOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VatNumber { get; set; }
        public DateTime CreatedFrom { get; set; }
        public DateTime CreatedTo { get; set; }
        public int CustomerId { get; set; }
    }
}
