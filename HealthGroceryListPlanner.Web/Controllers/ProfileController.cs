using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly GroceryContext _context;

        public ProfileController(GroceryContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(User model)
        {
            var userId = GetUserId();

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            user.Name = model.Name;
            user.Email = model.Email;
            user.Age = model.Age;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Profile updated";

            return RedirectToAction("Index");
        }
    }
}