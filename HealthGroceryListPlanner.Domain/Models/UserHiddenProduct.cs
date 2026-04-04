using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Domain.Models

{
    public class UserHiddenProduct
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}