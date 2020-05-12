using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    public class CustomerUpdateOptions
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
    }
}
