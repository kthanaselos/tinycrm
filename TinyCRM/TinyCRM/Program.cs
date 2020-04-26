using System;

namespace TinyCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dose afm");

            if (IsValidAfm(Console.ReadLine()))
            {
                Console.WriteLine("valid afm");
            }
            else
            {
                Console.WriteLine("NOT valid afm");
            }
        }

        public static bool IsValidAfm(string afm)
        {
            if (string.IsNullOrWhiteSpace(afm))
            {
                return false;
            }

            afm = afm.Trim();

            if (int.TryParse(afm, out int result) && afm.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsAdult(int age)
        {
            if (age >= 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            email = email.Trim();

            if (email.Contains("@") && email.EndsWith(".com"))
            {
                return true;
            }

            return false;
        }
    }
}
