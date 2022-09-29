using Labb2F2;

public class Program
{
    static void Main(string[] args)
    {
        MainMenu();
    }

    public static void MainMenu()
    {
        bool showMenu = true;
        while (showMenu)
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("*** Welcome to my shopping store! ***");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("1) Register a new customer");
            Console.WriteLine("2) Sign in");
            Console.WriteLine("3) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShoppingStore.RegisterNewCustomer();
                    showMenu = false;
                    break;
                case "2":
                    ShoppingStore.SignIn();
                    showMenu = false;
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }
        }
    }

}
