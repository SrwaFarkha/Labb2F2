using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using StringManipulation;

namespace Labb2F2
{
    public class Customer
    {
        public string Name { get; private set; }
        private string Password { get; set; }

        public List<Product> Cart { get; set; }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            Cart = new List<Product>();
        }

        public static void RegisterNewCustomer()
        {
            Console.Clear();
            Console.WriteLine("***Register a new customer***");
            //Console.WriteLine("Write your name: ");
            //string name = Console.ReadLine();


            //Console.WriteLine("Write a new password: ");
            //string password = Console.ReadLine();
            Console.WriteLine("Enter your username:");
            var username = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();


            Console.WriteLine("Congratulations! You are now a customer to us. Click enter to continue");

            MainMenu2();

        }

        public static SecureString SignIn()
        {
            Console.Clear();
            Console.WriteLine("Write your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Write you password: ");
            SecureString password = new SecureString();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
            } 
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                return password;

            }
            
            MainMenu2();

        }

        

        public static void Main2(string[] args)
        {
            bool showMenu2 = true;
            while (showMenu2)
            {
                showMenu2 = MainMenu2();
            }
        }
        public static bool MainMenu2()
        {
            Console.Clear();
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Shopping");
            Console.WriteLine("2) Shopping cart");
            Console.WriteLine("3) Check out");
            Console.WriteLine("4) Exit");


            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    //Shopping.Product();
                    return true;
                case "2":
                    //ShoppingCart.SelectedProduct();
                    return true;
                    //case "3":
                    //    CheckOut...
                    return true;
                case "4":
                    return false;
                default:
                    return true;
            }
        }


    }
}
