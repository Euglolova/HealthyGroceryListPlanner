using HealthyGroceryListPlanner.Services;
using HealthyGroceryListPlanner.UI;

namespace HealthyGroceryListPlanner.UI;

public class HomeScreen
{
    private readonly GroceryService _service;

    public HomeScreen (GroceryService service)
    {
        _service = service;
    }


    public void Show()
    {
        while (true)
        {

            Console.Clear(); //"clean screen"
            Console.WriteLine("=== Healthy Grocery List Planner ===");
            Console.WriteLine("1. View grocery list");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Mark as purchased");
            Console.WriteLine("4. Exit");

            Console.WriteLine();
            Console.Write("Choose an option: 1 Screen or 2 Screen or 3 Screen or 4 Screen");
            Console.WriteLine();
            var choice  = Console.ReadLine();                
            

            if (choice  == "1")
            {
                var groceryListScreen = new GroceryListScreen (_service);
                groceryListScreen.Show();
            }
            
            else if (choice == "2")
            {
                var addProductScreen = new AddProductScreen(_service);
                addProductScreen.Show();
                }

            else if (choice == "3")
            {
                var toggleScreen = new TogglePurchasedScreen(_service);
                toggleScreen.Show();
                }
                else if (choice == "4")
                {
                    Environment.Exit(0);
                    }
        }

    }

}