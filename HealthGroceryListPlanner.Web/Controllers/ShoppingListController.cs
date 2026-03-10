using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly GroceryContext _context;

        public ShoppingListController(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // Show All Shopping Lists
        // =========================
        public async Task<IActionResult> Index()
        {
            var lists = await _context.ShoppingLists
                .Include(l => l.Products)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            return View(lists);
        }

        // =========================
        // Create Shopping List (GET)
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // Create Shopping List (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShoppingList list)
        {
            if (!ModelState.IsValid)
                return View(list);

            list.CreatedAt = DateTime.Now;

            _context.ShoppingLists.Add(list);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Delete Shopping List
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var list = await _context.ShoppingLists.FindAsync(id);

            if (list != null)
            {
                _context.ShoppingLists.Remove(list);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}