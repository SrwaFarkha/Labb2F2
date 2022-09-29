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

        public Customer(string username, string password, List<Product> cart, Enums.CustomerLevelType customerLevelType)
        {
            Username = username;
            Password = password;
            Cart = cart;
            CustomerLevelType = customerLevelType;
        }

        public static void SaveCustomerToTxt(Customer customer)
        {
            using FileStream fileStream = File.Open("CustomerDetails.txt", FileMode.Append);
            fileStream.Close();

            string json = File.ReadAllText("CustomerDetails.txt");

            if (json == "")
            {
                List<Customer> customerJson = new List<Customer>();
                customerJson.Add(new Customer(customer.Username, customer.Password, customer.Cart, customer.CustomerLevelType));
                var convertedJson = JsonConvert.SerializeObject(customerJson, Formatting.Indented);

                System.IO.File.WriteAllText("CustomerDetails.txt", convertedJson);
            }
            else if (json != "")
            {
                var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);
                customerJson?.Add(new Customer(customer.Username, customer.Password, customer.Cart, customer.CustomerLevelType));
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

        public static List<Customer> GetCustomersFromTxt()
        {
            var json = File.ReadAllText("CustomerDetails.txt");
            var customerJson = JsonConvert.DeserializeObject<List<Customer>>(json);

            return customerJson;
        }

        public static Customer GetCustomerFromTxt(Customer customer)
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
