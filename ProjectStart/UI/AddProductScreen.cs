using HealthyGroceryListPlanner.Models;
using HealthyGroceryListPlanner.Services;


namespace HealthyGroceryListPlanner.UI;

public class AddProductScreen
{
    private readonly GroceryService _service; //This screen stores a link to the service for adding products.

    public AddProductScreen(GroceryService service)
{
    _service = service;
}
public void Show()                    //main screen method.
    {
        Console.Clear();
        Console.WriteLine("=== Add Product ===");
        Console.WriteLine();

        Console.Write("Enter product name: ");
        var name = Console.ReadLine();
        
        Console.Write("Enter quantity: ");
        var quantityInput = Console.ReadLine();
        int quantity = int.Parse(quantityInput ?? "0"); //?? "0" - if empty, use 0

        Console.Write("Enter category: ");
        var category = Console.ReadLine();

        var newProduct = new Product
        {
            Id = GenerateId(), //create a unique ID
            Name = name ?? "", //if null, replace with an empty string
            Quantity = quantity,
            Category = category ?? ""
        };
                _service.AddProduct(newProduct);

        Console.WriteLine();
        Console.WriteLine("Product added successfully!");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private int GenerateId()
    {
        var products = _service.GetAllProducts();
        return products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
    }
}
   