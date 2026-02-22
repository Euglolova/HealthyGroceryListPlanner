using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsPurchased { get; set; }
    }
}