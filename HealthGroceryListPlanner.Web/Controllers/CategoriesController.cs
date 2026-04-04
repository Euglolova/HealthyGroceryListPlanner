using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    // 🔒 Only authenticated users can access categories
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly GroceryContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoriesController(GroceryContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // =========================
        // GET CURRENT USER ID
        // =========================
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }

        // =========================
        // GET: /Categories
        // =========================
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();

            // Get categories (global + user-owned, excluding deleted)
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted && (c.IsGlobal || c.UserId == userId))
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }

        // =========================
        // GET: /Categories/Details/5
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            // Get category with products
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    !c.IsDeleted &&
                    (c.IsGlobal || c.UserId == userId));

            if (category == null)
                return NotFound();

            // =========================
            // GET HIDDEN PRODUCTS FOR USER
            // =========================
            var hiddenIds = await _context.UserHiddenProducts
                .Where(h => h.UserId == userId)
                .Select(h => h.ProductId)
                .ToListAsync();

            // =========================
            // FILTER PRODUCTS
            // =========================
            category.Products = category.Products
                .Where(p =>
                    (p.IsGlobal || p.UserId == userId) && // user + global
                    p.ShoppingListId == null &&          
                    !hiddenIds.Contains(p.Id))            // exclude hidden
                .OrderBy(p => p.Name)
                .ToList();

            return View(category);
        }

        // =========================
        // CREATE CATEGORY
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(category);

            var userId = GetUserId();

            // Assign ownership
            category.UserId = userId;
            category.IsGlobal = false;

            // =========================
            // IMAGE UPLOAD
            // =========================
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(stream);

                category.ImageUrl = "/images/" + fileName;
            }
            else
            {
                // Default image fallback
                category.ImageUrl = "/images/default.jpg";
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE CATEGORY (SOFT DELETE)
        // =========================

        // GET
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            if (category.IsDeleted)
                return NotFound();

            // Security: user can delete only their own categories
            if (category.UserId != userId)
                return Forbid();

            return View(category);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            if (category.UserId != userId)
                return Forbid();

            // Soft delete (hide category instead of removing from DB)
            category.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}