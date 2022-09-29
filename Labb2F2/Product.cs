using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2F2
{
    public class Product
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product("Apple", 8.0M),
            new Product("Banana", 10.0M),
            new Product("Pear", 12.0M)
        };

        public Product(string productName, decimal price)
        {
            ProductName = productName;
            Price = price;
        }

        public decimal GetPrice(Customer customer)
        {
            switch (customer.CustomerLevelType)
            {
                case Enums.CustomerLevelType.Bronze:
                    return Price - (Price * 5 / 100);
                case Enums.CustomerLevelType.Silver:
                    return Price - (Price * 10 / 100);
                case Enums.CustomerLevelType.Gold:
                    return Price - (Price * 15 / 100);
                default:
                    return Price;
            }
        }

    }
}
