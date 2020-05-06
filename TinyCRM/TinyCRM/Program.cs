using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TinyCRM
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    string[] file;

        //    try
        //    {
        //        file = File.ReadAllLines("products.csv");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"{ex.Message}");
        //        return;
        //    }

        //    if (file.Length == 0)
        //    {
        //        return;
        //    }

        //    var productList = new List<Product>();
        //    var random = new Random();

        //    foreach (string line in file)
        //    {
        //        //Console.WriteLine(line); // testing if file was read successfully

        //        if (string.IsNullOrWhiteSpace(line))
        //        {
        //            return;
        //        }
        //        line.Trim();

        //        var subLine = line.Split(";");


        //        //if (IsProductIdUnique(subLine[0], productList))
        //        if (!productList.Where(product => product.ProductId.Equals(subLine[0])).Any()) //Linq
        //        {
        //            {
        //                var product = new Product();

        //                product.ProductId = subLine[0];
        //                product.Name = subLine[1];
        //                product.Description = subLine[2];
        //                product.Price = (decimal)Math.Round(random.NextDouble() * 100, 2);

        //                productList.Add(product);
        //            }
        //        }
        //    }

        //    //PrintProductList(productList); // Kanoume print th lista gia na doyme oti ola phgan kala


        //    //Start Of Assignment for 2 May 2020
        //    var konstantinos = new Customer("140588636");
        //    var nikolas = new Customer("123456789");

        //    var konstantinosNewOrder = new Order();
        //    var nikolasNewOrder = new Order();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        konstantinosNewOrder.ProductList.Add(productList[random.Next(productList.Count)]);
        //        nikolasNewOrder.ProductList.Add(productList[random.Next(productList.Count)]);
        //    }

        //    konstantinos.OrderList.Add(konstantinosNewOrder);
        //    nikolas.OrderList.Add(nikolasNewOrder);

        //    if (konstantinos.TotalGross > nikolas.TotalGross)
        //    {
        //        Console.WriteLine("O Konstantinos einai o most valueable customer");
        //    }
        //    else if (konstantinos.TotalGross < nikolas.TotalGross)
        //    {
        //        Console.WriteLine("O Nikolas einai o most valueable customer");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Einai to idio valuable");
        //    }

        //    var customers = new List<Customer>();

        //    customers.Add(konstantinos);
        //    customers.Add(nikolas);

        //    CalculateTopFiveMostSoldProducts(customers, productList);
        //}

        //public static void CalculateTopFiveMostSoldProducts(List<Customer> customers, List<Product> products)
        //{
        //    foreach (var c in customers)
        //    {
        //        foreach (var o in c.OrderList)
        //        {
        //            foreach (var p in o.ProductList)
        //            {
        //                p.SoldQuantity++;
        //                //Console.WriteLine(products.IndexOf(p));
        //            }
        //        }
        //    }

        //    var SortedList = products.OrderByDescending(p => p.SoldQuantity).ToList().Take(5);

        //    foreach (var p in SortedList)
        //    {
        //        Console.WriteLine($"{ p.ProductId} is one of the top 5 with {p.SoldQuantity} items sold.");
        //    }
        //}

        //public static bool IsProductIdUnique(string id, List<Product> list)
        //{
        //    foreach (Product p in list)
        //    {
        //        if (p.ProductId.Equals(id))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //public static void PrintProductList(List<Product> list)
        //{
        //    foreach (Product p in list)
        //    {
        //        Console.WriteLine($"{p.ProductId} || {p.Name} || {p.Description} || {p.Price}");
        //    }
        //}

        static void Main(string[] args)
        {
            //var tinyCrmDbContext = new TinyCrmDbContext();

            //// insert
            //var customer = new Customer()
            //{
            //    Firstname = "Konstantinos",
            //    Lastname = "Thanaselos",
            //    Email = "kthanaselos@gmail.com"
            //};

            //tinyCrmDbContext.Add(customer);
            //tinyCrmDbContext.SaveChanges();

            //// Get data
            //var customer2 = tinyCrmDbContext
            //    .Set<Customer>()
            //    .Where(c => c.CustomerId == 2)
            //    .ToList()
            //    .SingleOrDefault();

            //// Update data
            //customer2.Phone = "6976512900";
            //tinyCrmDbContext.SaveChanges();

            //// Delete
            //tinyCrmDbContext.Remove(customer2);
            //tinyCrmDbContext.SaveChanges();


            //START OF ASSIGNMENT FOR 7 MAY 2020

            var custOptions = new CustomerSearchOptions()
            {
                FirstName = "Konstantinos",
                LastName = "Thanaselos",
                VatNumber = "123456789",
                CreatedFrom = new DateTime(2020, 5, 5),
                CreatedTo = new DateTime(2020, 5, 6),
                CustomerId = 2
            };

            var result1=SearchCustomers(custOptions);

            var prodOptions = new ProductSearchOptions()
            {
                ProductId = null,
                PriceFrom = 21.42m,
                PriceTo = 50m,
                Categories = { "televisions", "smartphones" }
            };

            var result2 = SearchProducts(prodOptions);
        }

        public static List<Customer> SearchCustomers(CustomerSearchOptions options) // Παραδοχή υλοποιησης: Εχει δωθεί τιμή σε όλα τα searchOptions. Κακή παραδοχή ..."I know", για αυτο skip στην SearchProducts() που ειναι πιο σωστη
        {
            using var tinyCrmDbContext = new TinyCrmDbContext();

            var result = tinyCrmDbContext
                .Set<Customer>()
                .Where(c => (c.CustomerId == options.CustomerId) ||
                            (c.Firstname == options.FirstName) ||
                            (c.Lastname == options.LastName) ||
                            (c.VatNumber == options.VatNumber) ||
                            ((c.Created >= options.CreatedFrom) && (c.Created <= options.CreatedTo)))
                .Take(500)
                .ToList();
            return result;
        }

        public static List<Product> SearchProducts(ProductSearchOptions options) // Παραδοχή υλοποίησης: Μπορεί να ΜΗΝ εχει δωθεί τιμή σε ΟΛΑ τα searchOptions
        {
            using var tinyCrmDbContext = new TinyCrmDbContext();

            var result1 = new Product();
            if (!string.IsNullOrWhiteSpace(options.ProductId)) //ψαχνουμε το προφανως μοναδικο product με βαση ProdustId ΑΝ μας έχει δωθεί. (Παρατηρηση: Εδώ το Productid είναι ακόμα string λόγω της προηγουμενης ασκησης και οχι integer)
            {
                result1 = tinyCrmDbContext.Set<Product>().Where(p => p.ProductId == options.ProductId).SingleOrDefault();
            }

            var result2 = new List<Product>();
            if ((options.PriceFrom != null && options.PriceTo != null)) //ψαχνουμε τα products αναλογα με τα 2 όρια τιμων που εχουν δωθεί 
            {
                result2 = tinyCrmDbContext
                    .Set<Product>()
                    .Where(p => (p.Price >= options.PriceFrom) && (p.Price <= options.PriceTo))
                    .Take(500)
                    .ToList();
            }
            else if (options.PriceFrom != null)//ψαχνουμε τα products αναλογα με το κάτω όριο τιμης που εχει δωθει
            {
                result2 = tinyCrmDbContext
                    .Set<Product>()
                    .Where(p => (p.Price >= options.PriceFrom))
                    .Take(500)
                    .ToList();
            }
            else if (options.PriceTo != null)//ψαχνουμε τα products αναλογα με το άνω όριο τιμης που εχει δωθει
            {
                result2 = tinyCrmDbContext
                    .Set<Product>()
                    .Where(p => (p.Price <= options.PriceTo))
                    .Take(500)
                    .ToList();
            }

            var result3 = new List<Product>();
            if (options.Categories != null) //ελεγχουμε αν μας δωθηκε κατηγορία/ες προιοντων
            {
                foreach (var categ in options.Categories) //για καθε category που μας δωθηκε φερνουμε τα αποτελεσματα απο τη βαση
                {
                    var tempResult = tinyCrmDbContext
                                    .Set<Product>()
                                    .Where(p => (p.ProductCategory == categ))
                                    .Take(500)
                                    .ToList();
                    if (tempResult.Count != 0) //αν υπηρχαν αποτελεματα απο καθε κατηγορια τα βαζουμε σε μια κοινη λιστα
                    {
                        result3.AddRange(tempResult);
                    }
                }
            }

            var finalResult = new List<Product>(); // φτιαχνουμε την τελικη λιστα που θα επιστρεψουμε αφου ελεγξουμε οτι η βαση μας εδωσε αποτελεσματα για καθε περιπτωση.

            if (result1 != null)
            {
                finalResult.Add(result1);
            }
            if (result2.Count != 0)
            {
                finalResult.AddRange(result2);
            }
            if (result3.Count != 0)
            {
                finalResult.AddRange(result3);
            }

            return finalResult.Take(500).ToList();
        }
    }
}
