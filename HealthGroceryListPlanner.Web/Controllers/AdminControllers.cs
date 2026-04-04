using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;
using System.Security.Claims;

namespace HealthGroceryListPlanner.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly GroceryContext _context;

        public AdminController(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // GET CURRENT USER ID
        // =========================
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null || string.IsNullOrEmpty(claim.Value))
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }

        // =========================
        // USERS LIST
        // =========================
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .OrderBy(u => u.Name)
                .ToListAsync();

            return View(users);
        }

        // =========================
        // EDIT USER (GET)
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // =========================
        // EDIT USER (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
                return NotFound();

            // 🔥 обновляем только нужное
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }

        // =========================
        // DELETE USER (GET)
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // =========================
        // DELETE USER (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = GetUserId();


            if (id == currentUserId)
                return BadRequest("You cannot delete yourself");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Users));
        }
    }
}