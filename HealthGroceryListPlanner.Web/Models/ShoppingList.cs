using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Web.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; } = new();
    }
}