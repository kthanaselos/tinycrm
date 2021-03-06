﻿namespace TinyCrm.Core.Model
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int SoldQuantity { get; set; }
        public ProductCategory  Category { get; set;}
    }
}
