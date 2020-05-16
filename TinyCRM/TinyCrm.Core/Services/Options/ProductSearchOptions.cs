using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Services.Options
{
    public class ProductSearchOptions
    {
        public string ProductId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public List<String> Categories { get; set; } 
    }
}