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

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0.01, 1000, ErrorMessage = "Quantity must be between 0.01 and 1000")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Please select a unit")]
        public UnitType Unit { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsPurchased { get; set; }
    }
}