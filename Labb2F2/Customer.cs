using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Labb2F2
{
    public class Customer
    {
        public string Username { get; private set; }
        public string Password { get; set; }
        public Enums.CustomerLevelType CustomerLevelType { get; set; }
        public List<Product> Cart { get; set; }


        // Konstruktorn används till att sätta värde på propertis när vi gör en ny instans av klassen
        public Customer(string username, string password, List<Product> cart, Enums.CustomerLevelType customerLevelType)
        {
            Username = username;
            Password = password;
            Cart = cart;
            CustomerLevelType = customerLevelType;
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

            var customers = GetCustomersFromTxt(); //hämtar ut alla customers från text filen

            var customer = customers.FirstOrDefault(x => x.Username == username && x.Password == password);

            //checkCustomerExists: kollar om kunden finns och användaren har skrivit in rätt användarnamn och lösenord
            var checkCustomerExists = customers.Any(x => x.Username == username && x.Password == password);
            //checkCustomerIncorrectPassword: kollar om användaren skrivit in rätt namn men fel lösenord
            var checkCustomerIncorrectPassword = customers.Any(x => x.Username == username && x.Password != password);
            //cehckCustomerNotExist: Kollar om kontot finns eller inte
            var checkCustomerNotExists = customer == null;

            if (checkCustomerExists)
            {
                ShoppingStore.ShoppingStoreMain(customer);
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
                        ShoppingStore.RegisterNewCustomer();
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

        public static void SaveCustomerToTxt(Customer customer)
        {
            using FileStream fileStream = File.Open("CustomerDetails.txt", FileMode.Append); // Om filen inte finns, skapa
            fileStream.Close();

            string json = File.ReadAllText("CustomerDetails.txt"); // läser filen

            if (json == "")// om json(textfilen) är tom 
            {
                //skapa en ny lista av customer
                List<Customer> customerJson = new List<Customer>();
                //lägger till en customer i listan
                customerJson.Add(customer);

                //konverta listan till json objekt
                var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);
                //sparar json objekt till text filen
                System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
            }
            else if (json != "")
            {
                var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);
                customerJson.Add(customer);
                var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);

                System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
            }
        }

        public static void SaveProductToTxt(Customer customer)
        {
            string json = File.ReadAllText("CustomerDetails.txt");
            var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);

            var cus = customerJson.FirstOrDefault(x => x.Username == customer.Username);
            cus.Cart = customer.Cart;

            var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);

            System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
        }


        public static void DeleteAllProductFromCartTxt(Customer customer)
        {
            string json = File.ReadAllText("CustomerDetails.txt");
            var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);

            var cus = customerJson.FirstOrDefault(x => x.Username == customer.Username);
            cus.Cart = new List<Product>();
            var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);

            System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
        }

        public static List<Customer> GetCustomersFromTxt()//retunerar en lista av customers
        {
            using FileStream fileStream = File.Open("CustomerDetails.txt", FileMode.Append); // Om filen inte finns, skapa
            fileStream.Close();

            var json = File.ReadAllText("CustomerDetails.txt");

            var customers = JsonConvert.DeserializeObject<List<Customer>>(json);

            return customers;
        }

        public static Customer GetCustomerFromTxt(Customer customer)// retunerar en customer beroende på username som skickas in i metoden 
        {
            var json = File.ReadAllText("CustomerDetails.txt");
            var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);
            var cus = customerJson.FirstOrDefault(x => x.Username == customer.Username);
            
            return cus;
        }

        public static void PopulateCustomers()
        {
            List<Customer> customerList = new List<Customer> //Fördefinerade kunder
            {
                new Customer("knatte", "123", new List<Product>(), Enums.CustomerLevelType.Gold),
                new Customer("fnatte", "321", new List<Product>(), Enums.CustomerLevelType.Silver),
                new Customer("tjatte", "213", new List<Product>(), Enums.CustomerLevelType.Bronze)
            };
            using FileStream fileStream = File.Open("CustomerDetails.txt", FileMode.Append);
            fileStream.Close();

            string json = File.ReadAllText("CustomerDetails.txt");

            if (json == "")
            {
                List<Customer> customerJson = new List<Customer>();
                foreach (var customer in customerList)
                {
                    customerJson.Add((customer));
                }

                var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);
                System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
            }
        }

        public override string ToString()
        {
            return Username.ToString() + Password.ToString() + Cart.ToString();
        }
    }
}
