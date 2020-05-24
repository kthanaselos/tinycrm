using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Services.Options;

namespace TinyCRM
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static void PopulateProductsByFile()
        {
            string[] file;

            try
            {
                file = File.ReadAllLines("products.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return;
            }

            if (file.Length == 0)
            {
                return;
            }

            var productList = new List<Product>();
            var random = new Random();

            foreach (string line in file)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    return;
                }
                line.Trim();

                var subLine = line.Split(";");

                if (!productList.Where(product => product.ProductId.Equals(subLine[0])).Any()) //Linq
                {
                    {
                        var product = new Product();

                        product.ProductId = subLine[0];
                        product.Name = subLine[1];
                        product.Description = subLine[2];
                        product.Price = (decimal)Math.Round(random.NextDouble() * 100, 2);

                        productList.Add(product);
                    }
                }
                using (var context = new TinyCrmDbContext())
                {
                    IProductService productService = new ProductService(context);

                    foreach (var product in productList)
                    {
                        var customer = productService.CreateProduct(new CreateProductOptions()
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Description = product.Description,
                            Price = product.Price,
                        });
                    }
                };
            }

            foreach (Product p in productList)
            {
                Console.WriteLine($"{p.ProductId} || {p.Name} || {p.Description} || {p.Price}");
            }
        }
    }
}
