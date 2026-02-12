using HealthyGroceryListPlanner.Services;

namespace HealthyGroceryListPlanner.UI;
public class TogglePurchasedScreen
{
    private readonly GroceryService _service;

    public TogglePurchasedScreen(GroceryService service)
    {
        _service = service;
    }
    
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("=== Mark as Purchased ===");
        Console.WriteLine();

        var products = _service.GetAllProducts();

        foreach (var product in products)
        {
            //IsPurchased == true → [X] else → [ ]
            string status = product.IsPurchased ? "[X]" : "[ ]";
            Console.WriteLine($"{status} {product.Id}. {product.Name}");
        }

        Console.WriteLine();
        Console.Write("Enter product ID to toggle: ");
        var input = Console.ReadLine(); //The user chooses which product to change.

        if (int.TryParse(input, out int id))
        {
            _service.TogglePurchased(id);
            Console.WriteLine("Updated successfully!");
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}