using Labb2F2;
using Newtonsoft.Json;

public class Program
{
    static void Main(string[] args)//anropar main metoden. Start punkten på applikationen
    {
        var customer = Customer.GetCustomersFromTxt();
        if (customer == null)
        {
            Customer.PopulateCustomers();
        }
        
        MainMenu();
    }

    public static void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("*** Welcome to my shopping store! ***");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1) Register a new customer");
            Console.WriteLine("2) Sign in");
            Console.WriteLine("3) Exit");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())//switch kollar av user input. Beroende på vad användaren väljer så navigeras den till olika classers metoder
            {
                case "1":
                    ShoppingStore.RegisterNewCustomer();
                    break;
                case "2":
                    ShoppingStore.SignIn();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }
        }
    }

}
