using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    public class ProductUpdateOptions
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
