using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;

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
        public IActionResult Index()
        {
            var lists = _context.ShoppingLists
                .Include(l => l.Products)
                .OrderByDescending(l => l.CreatedAt)
                .ToList();

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
        public IActionResult Create(ShoppingList list)
        {
            if (ModelState.IsValid)
            {
                _context.ShoppingLists.Add(list);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(list);
        }
    }
}