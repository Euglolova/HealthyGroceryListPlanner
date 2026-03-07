using System.ComponentModel.DataAnnotations;
using HealthGroceryListPlanner.Web.Models.Enums;

namespace HealthGroceryListPlanner.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; } = "";

        // Теперь целое число
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int? Quantity { get; set; }

        // Не обязательное поле
        public UnitType? Unit { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsPurchased { get; set; }

        public string Emoji { get; set; } = "";

        public int? ShoppingListId { get; set; }

        public ShoppingList? ShoppingList { get; set; }
    }
}