namespace HealthyGroceryListPlanner.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = ""; //= "" —default value (empty string)

    public int Quantity { get; set; } //product quantity (кол-во)

    public string Category { get; set; } = ""; //= "" — error protection

    public bool IsPurchased { get; set; } //shows: the product has been purchased or not (true/false)
}

/* view 
var product = new Product
{
    Id = 1,
    Name = "Apples",
    Quantity = 2,
    Category = "Fruits",
    IsPurchased = false
};*/