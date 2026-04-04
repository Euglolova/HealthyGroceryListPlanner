using HealthGroceryListPlanner.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace HealthGroceryListPlanner.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(GroceryContext context)
        {
            if (!context.Users.Any(u => u.Email == "kokolova02eugenia@gmail.com"))
            {

            var admin = new User
            {
                Name = "Admin",
                Email = "kokolova02eugenia@gmail.com",
                PasswordHash = Hash("admin123"),
                Role = "Admin"
            };

            context.Users.Add(admin);
            context.SaveChanges();
            }
        }

        private static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }
    }
}