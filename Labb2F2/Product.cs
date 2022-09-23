using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2F2
{
    public class Product
    {
        //public string ProductName { get; set; }
        //public int Price { get; set; }
        public void Run()
        {
            DisplayOutro();
        }


        private void DisplayIntro()
        {

        }

        private void SellItem()
        {

        }

        private void DisplayOutro()
        {
            Console.WriteLine("Thanks for shopping!");
            Console.ReadKey();
        }
    }


}
