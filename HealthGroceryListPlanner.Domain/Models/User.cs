using System.ComponentModel.DataAnnotations;

namespace HealthGroceryListPlanner.Domain.Models
{
    public class User
    {
        public string Role { get; set; } = "User";
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}