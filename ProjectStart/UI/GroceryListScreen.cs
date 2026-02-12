using HealthyGroceryListPlanner.Services;

namespace HealthyGroceryListPlanner.UI;

public class GroceryListScreen
{
    private readonly GroceryService _service;
    public GroceryListScreen(GroceryService service)
    {
        _service = service;
    }
    public void Show()
    {
        Console.Clear();
        Console.WriteLine("=== Grocery List ===");

        var products = _service.GetAllProducts();

        foreach (var product in products)
        {
            string status = product.IsPurchased ? "[X]" : "[ ]";
            Console.WriteLine($"{status} {product.Id}. {product.Name} ({product.Quantity}) - {product.Category}");
        }
        
        Console.WriteLine();
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}