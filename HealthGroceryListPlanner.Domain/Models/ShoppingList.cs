using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Domain.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; } = new();
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}