using System.Collections.Generic;

namespace HealthGroceryListPlanner.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}