using System;

namespace TinyCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var kthanaselos = new Customer("123456789");
            } catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            
            
        }
    }
}
