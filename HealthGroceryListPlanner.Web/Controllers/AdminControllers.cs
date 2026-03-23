using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    [Authorize(Roles = "Admin")] // just admin
    public class AdminController : Controller
    {
        private readonly GroceryContext _context;

        public AdminController(GroceryContext context)
        {
            _context = context;
        }

        // ================= USER LIST =================
        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // ================= EDIT USER =================
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
                return NotFound();

            // 🔥 обновляем только нужные поля
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            _context.SaveChanges();

            return RedirectToAction("Users");
        }

        // ================= DELETE USER =================
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Users");
        }
    }
}