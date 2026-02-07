using HealthyGroceryListPlanner.Models;  //using classes from the Models folder

namespace HealthyGroceryListPlanner.Services;

public class GroceryService
{
    //private readonly - accessible only internally, the list cannot be replaced with another one
    //new() — creates an empty list
    private readonly List<Product> _products = new(); 
    
    //A method that: returns a list of products; used by the UI
    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product); //Add product to list
    }

    public void RemoveProduct(int productId) //Method for deleting a product by ID.
    {
        //var — C# will automatically determine the type
        // FirstOrDefault — take the first matching element or null
        // p => p.Id == productId — search condition (lambda)
        
        var product = _products.FirstOrDefault(p => p.Id == productId);
        
        
        if (product != null) //check
        {
            _products.Remove(product);
        }
    }

}