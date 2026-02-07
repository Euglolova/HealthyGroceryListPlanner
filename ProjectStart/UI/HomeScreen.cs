using HealthyGroceryListPlanner.UI;

namespace HealthyGroceryListPlanner.UI;

public class HomeScreen
{
    public void Show()
    {
        while (true)
        {

            Console.Clear(); //"clean screen"
            Console.WriteLine("=== Healthy Grocery List Planner ===");
            Console.WriteLine("1. View grocery list");
            Console.WriteLine("2. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: 1 or 2 ");
            Console.WriteLine();
            var choice  = Console.ReadLine();                
            

            if (choice  == "1")
            {
                var groceryListScreen = new GroceryListScreen ();
                groceryListScreen.Show();
            }

            else if (choice == "2")
            {
                Environment.Exit(0);
            }


        }
        
    }


}