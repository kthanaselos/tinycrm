using System;
using System.Collections.Generic;
using System.IO;

namespace TinyCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string[] file = File.ReadAllLines(@"C:\Users\kosta\Desktop\ProductList.txt");

                var productList = new List<Product>();
                var random = new Random();

                foreach (string line in file)
                {
                    //Console.WriteLine(line); // testing if file was read successfully

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        return;
                    }
                    line.Trim();

                    var subLine = line.Split(";");

                    if (!IsProductIdUnique(subLine[0], productList))
                    {
                        return;
                    }

                    var product = new Product();

                    product.ProductId = subLine[0];
                    product.Name = subLine[1];
                    product.Description = subLine[2];
                    product.Price = random.Next(1, 9000); //random akeraia timh apo 1 mexri 8999

                    productList.Add(product);
                }

                PrintProductList(productList); // Kanoume print th lista gia na doyme oti ola phgan kala
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            

           
        }

        public static bool IsProductIdUnique(string id, List<Product> list)
        {
            foreach (Product p in list)
            {
                if (string.Equals(id, p.Name[0]))
                {
                    return false;
                }
            }
            return true;
        }

        public static void PrintProductList(List<Product> list)
        {
            foreach (Product p in list)
            {
                Console.WriteLine($"{p.ProductId} || {p.Name} || {p.Description} || {p.Price}");
            }
        }
    }
}
