namespace HealthGroceryListPlanner.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int Quantity { get; set; }

        public string Category { get; set; } = "";

        public bool IsPurchased { get; set; }
    }
}