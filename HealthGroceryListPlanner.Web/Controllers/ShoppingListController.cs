using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    
    [Authorize] 
    public class ShoppingListController : Controller
    {
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }
        private readonly GroceryContext _context;

        public ShoppingListController(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // Show Lists (User vs Admin)
        // =========================
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            var lists = await _context.ShoppingLists
                .Include(l => l.Products)
                .Where(l => l.UserId == userId) 
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            return View(lists);
        }

        // =========================
        // Create (GET)
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // Create (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShoppingList list)
        {
            if (!ModelState.IsValid)
                return View(list);

            var userId = GetUserId();

            list.CreatedAt = DateTime.Now;
            list.UserId = userId; 

            _context.ShoppingLists.Add(list);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // Delete (защита!)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var list = await _context.ShoppingLists
                .Include(l => l.Products)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list == null)
                return NotFound();

            
            if (list.UserId != userId && role != "Admin")
            {
                return Forbid();
            }

            foreach (var product in list.Products)
            {
                product.ShoppingListId = null;
            }

            _context.ShoppingLists.Remove(list);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}