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

        // Сделали НЕ обязательным
        public decimal? Quantity { get; set; }

        // Сделали НЕ обязательным
        public UnitType? Unit { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsPurchased { get; set; }

        public string Emoji { get; set; } = "";
    }
}