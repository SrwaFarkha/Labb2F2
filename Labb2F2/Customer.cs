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

        public override string ToString()
        {
            return Username.ToString() + Password.ToString() + Cart.ToString();
        }
    }
}
