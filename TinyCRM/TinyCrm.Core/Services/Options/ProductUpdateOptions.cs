﻿using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services.Options
{
    public class ProductUpdateOptions
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
