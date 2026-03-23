using HealthGroceryListPlanner.Domain.Models;
using HealthGroceryListPlanner.Infrastructure.Data;
using System.Security.Cryptography;
using System.Text;

namespace HealthGroceryListPlanner.Application.Services
{
    public class AuthService
    {
        private readonly GroceryContext _context;

        public AuthService(GroceryContext context)
        {
            _context = context;
        }

        public async Task<User?> Register(string name, string email, string password)
        {
            // 🔥 защита от дублей (case-insensitive)
            if (_context.Users.Any(u => u.Email.ToLower() == email.ToLower()))
                return null;

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = Hash(password),

                // 🔥 ДОБАВИЛИ РОЛЬ
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public User? Login(string email, string password)
        {
            var hash = Hash(password);

            return _context.Users
                .FirstOrDefault(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.PasswordHash == hash);
        }

        private string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }
    }
}