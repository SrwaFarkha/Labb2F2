using System.ComponentModel.Design;
using Labb2F2;

namespace StringManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            PopulateCustomer();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
                //break;
            }

        }

        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Register new customer");
            Console.WriteLine("2) Log in");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Customer.RegisterNewCustomer();
                    return true;
                    
                case "2":
                    Customer.SignIn();
                    return true;
                    
                case "3":
                    return false;
                    
                default:
                    return true;
            }
        }

        public static void PopulateCustomer()
        {
            var arrCustomer = new Customer[]
            {
                new Customer("name1","password1"),
                new Customer("name2","password2"),
                new Customer("name3","password3")
            };
        }

    }
}