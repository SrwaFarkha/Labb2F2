using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Labb2F2
{
    public class ShoppingStore
    {
        public static Enums.CurrencyType Currency { get; set; } 

        public static void ShoppingStoreMain(Customer customer)
        {
            while (true)
            {
                Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("*** Shopping store main menu ***");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1) Go to shopping");
            Console.WriteLine("2) Show shopping cart");
            Console.WriteLine("3) Check out");
            Console.WriteLine("4) Sign out");

            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    StartShopping(customer);
                    break;
                    case "2":
                    ShowShoppingCart(customer);
                    break;
                case "3":
                    CheckOut(customer);
                    break;
                case "4":
                    Program.MainMenu();
                    break;

                }
            }
            
        }

        public static void RegisterNewCustomer()//metod för att registrera ny kund
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("***Register a new customer***");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Choose customer username, password and level\n");

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Default, Bronze, Silver or Gold: ");
            string inputCustomerType = Console.ReadLine().ToLower();

            var customerType = Enums.CustomerLevelType.Default;//deklarer en enum av customer type default
            switch (inputCustomerType)//baserat på user input sätter vi variabel customerType till den valda customerType
            {
                case "bronze":
                    customerType = Enums.CustomerLevelType.Bronze;
                    break;
                case "silver":
                    customerType = Enums.CustomerLevelType.Silver;
                    break;
                case "gold":
                    customerType = Enums.CustomerLevelType.Gold;
                    break;
                default:
                    customerType = Enums.CustomerLevelType.Default;
                    break;
            }

            Customer customer = new Customer(username, password, new List<Product>(), customerType);//ny instans av customer objekt baserat på user input
            Customer.SaveCustomerToTxt(customer);

            Console.WriteLine();
            Console.WriteLine("Congratulations! You are now a customer to us. Press any key to continue");
            Console.ReadKey();

            ShoppingStoreMain(customer);
        }

        public static void SignIn()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("*** Sign in page ***");
            Console.WriteLine("-----------------------------------");
            Console.Write("Enter your Username: ");
            string username = Console.ReadLine().ToLower();
            Console.Write("Enter your Password: ");
            string password = Console.ReadLine().ToLower();

            var customers = Customer.GetCustomersFromTxt(); //hämtar ut alla customers från text filen

            var customer = customers.FirstOrDefault(x => x.Username == username && x.Password == password);

            //checkCustomerExists: kollar om kunden finns och användaren har skrivit in rätt användarnamn och lösenord
            var checkCustomerExists = customers.Any(x => x.Username == username && x.Password == password); 
            //checkCustomerIncorrectPassword: kollar om användaren skrivit in rätt namn men fel lösenord
            var checkCustomerIncorrectPassword = customers.Any(x => x.Username == username && x.Password != password);
            //cehckCustomerNotExist: Kollar om kontot finns eller inte
            var checkCustomerNotExists = customer == null;

            if (checkCustomerExists)
            {
                ShoppingStoreMain(customer);
            }

            if (checkCustomerIncorrectPassword)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("*** Sign in page ***");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("\nIncorrect Password. Do you want to Try Again? [y/n]\n");
                    Console.Write("\nSelect an option: ");

                    var responese = Console.ReadKey().Key;
                    if (responese == ConsoleKey.Y)
                    {
                        SignIn();
                        break;
                    }

                    if (responese == ConsoleKey.N)
                    {
                        Program.MainMenu();
                        break;
                    }
                }
            }

            if (checkCustomerNotExists)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("*** Sign in page ***");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine($"There is no customer registrered with name: {username}. Do you want to register a new customer? [y/n]\n");
                    Console.Write("\nSelect an option: ");

                    var response = Console.ReadLine().ToLower();
                    if (response == "y")
                    {
                        RegisterNewCustomer();
                        break;
                    }

                    if (response == "n")
                    {
                        Program.MainMenu();
                        break;
                    }
                }
            }
        }

        public static void StartShopping(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("*** Shopping page ***");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Add product to cart by pressing the keys");


                int counter = 0;
                List<Product> products = new List<Product>
                {
                    new Product("Apple", 8.0M),
                    new Product("Banana", 10.0M),
                    new Product("Pear", 12.0M)
                };

                foreach (var product in products)
                {
                    counter++;
                    Console.WriteLine($"{counter}) {product.ProductName} Price: {GetPriceAndConvertCurrency(product.Price, Currency)}");
                }
                Console.WriteLine();
                Console.WriteLine("4) To check your shopping cart");
                Console.WriteLine("5) Back to Main Menu");
                Console.WriteLine("6) Change currency to SEK");
                Console.WriteLine("7) Change currency to USD");
                Console.WriteLine("8) Change currency to GBP");
                Console.Write("\nSelect an option: ");

                string userProductPick = Console.ReadLine();
                switch (userProductPick)
                {
                    case "1":
                        customer.Cart.Add(products[0]);
                        Customer.SaveProductToTxt(customer);
                        Console.WriteLine($"{products[0].ProductName} is added to your shopping cart! Press any key to continue shopping");
                        Console.ReadLine();
                        break;
                    case "2":
                        customer.Cart.Add(products[1]);
                        Customer.SaveProductToTxt(customer);
                        Console.WriteLine($"{products[1].ProductName} is added to your shopping cart! Press any key to continue shopping");
                        Console.ReadLine();
                        break;
                    case "3":
                        customer.Cart.Add(products[2]);
                        Customer.SaveProductToTxt(customer);                       
                        Console.WriteLine($"{products[2].ProductName} is added to your shopping cart! Press any key to continue shopping");
                        Console.ReadLine();
                        break;
                    case "4":
                        ShowShoppingCart(customer);
                        break;
                    case "5":
                        ShoppingStoreMain(customer);
                        break;
                    case "6":
                        Currency = Enums.CurrencyType.SEK;
                        break;
                    case "7":
                        Currency = Enums.CurrencyType.USD;
                        break;
                    case "8":
                        Currency = Enums.CurrencyType.GBP;
                        break;

                }
            }
        }

        public static void ShowShoppingCart(Customer customer)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"*** Shopping cart for {customer.Username} with customer level {customer.CustomerLevelType} ***");
                Console.WriteLine("-----------------------------------");

                var totalPrice = customer.Cart.Sum(x => x.GetPrice(customer));
                var groupedProducts = customer.Cart.GroupBy(x => x.ProductName).Select(group => new
                {
                    ProductName = group.Key,
                    Price = group.Sum(x => x.GetPrice(customer)),
                    Count = group.Count(),
                });

                foreach (var groupedProduct in groupedProducts)
                {
                    Console.WriteLine($"{groupedProduct.Count}x {groupedProduct.ProductName} {GetPriceAndConvertCurrency(groupedProduct.Price, Currency)}");
                }

                Console.WriteLine();
                Console.WriteLine($"Total price: {GetPriceAndConvertCurrency(totalPrice, Currency)}");

                Console.WriteLine();
                Console.WriteLine("1) Change currency to SEK");
                Console.WriteLine("2) Change currency to USD");
                Console.WriteLine("3) Change currency to GBP");
                Console.WriteLine("4) Back to Main Menu");
                Console.Write("\r\nSelect an option: ");

                string userNavigate = Console.ReadLine();
                switch (userNavigate)
                {
                    case "1":
                        Currency = Enums.CurrencyType.SEK;
                        break;
                    case "2":
                        Currency = Enums.CurrencyType.USD;
                        break;
                    case "3":
                        Currency = Enums.CurrencyType.GBP;
                        break;
                    case "4":
                        ShoppingStoreMain(customer);
                        break;
                }
            }

        }


        public static void CheckOut(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("*** Checkout ***");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Are you sure want to Checkout? [y/n]\n");
                Console.Write("\nSelect an option: ");

                var responese = Console.ReadKey().Key;
                if (responese == ConsoleKey.Y)
                {
                    Customer.DeleteAllProductFromCartTxt(customer);
                    Console.WriteLine("\n\nThank you for checking out. See you next time!");
                    Console.WriteLine("Press any key to Main Menu");
                    Console.ReadKey();
                    var cus = Customer.GetCustomerFromTxt(customer);
                    ShoppingStoreMain(cus);
                    break;
                }

                if (responese == ConsoleKey.N)
                {
                    ShoppingStoreMain(customer);
                    break;
                }
            }
        }

        public static string GetPriceAndConvertCurrency(decimal price, Enums.CurrencyType currency)
        {
            var cultureInfoString = "";
            switch (currency)
            {
                case Enums.CurrencyType.USD:
                    cultureInfoString = (price / 8).ToString("C", new CultureInfo("en-US")); //sätter pris och culturinfo baserat på enums currency type
                    break;
                case Enums.CurrencyType.GBP:
                    cultureInfoString = (price / 10).ToString("C", new CultureInfo("en-GB"));
                    break;
                default:
                    cultureInfoString = price.ToString("C", new CultureInfo("sv-SE"));
                    break;
            }

            return cultureInfoString;
        }
    }
}
