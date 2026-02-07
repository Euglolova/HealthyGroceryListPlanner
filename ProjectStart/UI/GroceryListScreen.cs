namespace HealthyGroceryListPlanner.UI;

public class GroceryListScreen
{
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("=== Grocery List ===");
        Console.WriteLine();
        Console.WriteLine("No products yet.");
        Console.WriteLine();
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}