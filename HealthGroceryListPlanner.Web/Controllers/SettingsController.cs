using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    [Authorize] 
        public class SettingsController : Controller
    {
        private readonly GroceryContext _context;

        public SettingsController(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // SAFE USER ID
        // =========================
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }

        // =========================
        // GET SETTINGS
        // =========================
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var settings = await _context.UserSettings
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (settings == null)
            {
                settings = new UserSettings
                {
                    UserId = userId,
                    Theme = "Light",
                    NotificationsEnabled = false,
                    AutoSaveEnabled = true,
                    ReminderFrequency = "Off"
                };

                _context.UserSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            return View(settings);
        }

        // =========================
        // SAVE SETTINGS
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserSettings model)
        {
            var userId = GetUserId();

            var settings = await _context.UserSettings
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (settings == null)
                return NotFound();

            settings.Theme = model.Theme;
            settings.NotificationsEnabled = model.NotificationsEnabled;
            settings.AutoSaveEnabled = model.AutoSaveEnabled;
            settings.ReminderFrequency = model.ReminderFrequency;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Settings saved";

            return RedirectToAction("Index");
        }

        // =========================
        // RELOAD BASE PRODUCTS
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReloadBaseProducts()
        {
            var userId = GetUserId();

            var userProducts = _context.Products
                .Where(p => p.UserId == userId && !p.IsGlobal);

            _context.Products.RemoveRange(userProducts);

            await _context.SaveChangesAsync();

            TempData["Message"] = "Base products reloaded";

            return RedirectToAction("Index");
        }

        // =========================
        // DELETE ACCOUNT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = GetUserId();

            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}