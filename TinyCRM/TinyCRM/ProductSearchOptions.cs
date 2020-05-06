using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCRM
{
    public class ProductSearchOptions
    {
        public string? ProductId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public List<String> Categories { get; set; } 
    }
}