using HealthyGroceryListPlanner.Models;
using HealthyGroceryListPlanner.Services;
using HealthyGroceryListPlanner.UI;


class Program
{
    static void Main(string[] args)
    {
        var service = new GroceryService();
        // Mocked data
        service.AddProduct(new Product { Id = 1, Name = "Apples", Quantity = 2, Category = "Fruits" });
        service.AddProduct(new Product { Id = 2, Name = "Milk", Quantity = 1, Category = "Dairy" });
        service.AddProduct(new Product { Id = 3, Name = "Carrots", Quantity = 3, Category = "Vegetables" });

        var homeScreen = new HomeScreen(service);
        homeScreen.Show();

        Console.ReadLine();
    }

    
}
