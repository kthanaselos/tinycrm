using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinyCRM
{
    class Program
    {
        static void Main(string[] args)
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
                //Console.WriteLine(line); // testing if file was read successfully

                if (string.IsNullOrWhiteSpace(line))
                {
                    return;
                }
                line.Trim();

                var subLine = line.Split(";");


                //if (IsProductIdUnique(subLine[0], productList))
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
            }

            //PrintProductList(productList); // Kanoume print th lista gia na doyme oti ola phgan kala


            //Start Of Assignment for 2 May 2020
            var konstantinos = new Customer("140588636");
            var nikolas = new Customer("123456789");

            var konstantinosNewOrder = new Order();
            var nikolasNewOrder = new Order();

            for (int i = 0; i < 10; i++)
            {
                konstantinosNewOrder.ProductList.Add(productList[random.Next(productList.Count)]);
                nikolasNewOrder.ProductList.Add(productList[random.Next(productList.Count)]);
            }

            konstantinos.OrderList.Add(konstantinosNewOrder);
            nikolas.OrderList.Add(nikolasNewOrder);

            if (konstantinos.TotalGross > nikolas.TotalGross)
            {
                Console.WriteLine("O Konstantinos einai o most valueable customer");
            }
            else if (konstantinos.TotalGross < nikolas.TotalGross)
            {
                Console.WriteLine("O Nikolas einai o most valueable customer");
            }
            else
            {
                Console.WriteLine("Einai to idio valuable");
            }

            var customers = new List<Customer>();

            customers.Add(konstantinos);
            customers.Add(nikolas);

            CalculateTopFiveMostSoldProducts(customers, productList);
        }

        public static void CalculateTopFiveMostSoldProducts(List<Customer> customers, List<Product> products)
        {
            foreach (var c in customers)
            {
                foreach (var o in c.OrderList)
                {
                    foreach (var p in o.ProductList)
                    {
                        p.SoldQuantity++;
                        //Console.WriteLine(products.IndexOf(p));
                    }
                }
            }

            var SortedList = products.OrderByDescending(p => p.SoldQuantity).ToList().Take(5);

            foreach (var p in SortedList)
            {
                Console.WriteLine($"{ p.ProductId} is one of the top 5 with {p.SoldQuantity} items sold.");
            }
        }

        public static bool IsProductIdUnique(string id, List<Product> list)
        {
            foreach (Product p in list)
            {
                if (p.ProductId.Equals(id))
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
