using System.Collections.Generic;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Emoji { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public User? User { get; set; }

        public bool IsGlobal { get; set; } = false;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}