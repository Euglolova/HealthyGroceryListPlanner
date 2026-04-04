using HealthGroceryListPlanner.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = "";

        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; } = 1;

        public UnitType? Unit { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsPurchased { get; set; }

        public string Emoji { get; set; } = "";

        public int? ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public bool IsGlobal { get; set; } = false;
    }
}