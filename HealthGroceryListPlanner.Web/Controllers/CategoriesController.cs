using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    [Authorize] // 🔥 только для залогиненных
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

            var categories = await _context.Categories
                .Where(c => c.IsGlobal || c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(categories);
        }

        // =========================
        // GET: /Categories/Details/5
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            var userId = GetUserId();

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    (c.IsGlobal || c.UserId == userId));

            if (category == null)
                return NotFound();

            // 🔥 фильтр продуктов
            category.Products = category.Products
                .Where(p => p.IsGlobal || p.UserId == userId)
                .OrderBy(p => p.Name)
                .ToList();

            return View(category);
        }

        // =========================
        // CREATE CATEGORY
        // =========================

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return View(category);

            var userId = GetUserId();

            // 🔥 привязка к пользователю
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

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                category.ImageUrl = "/images/" + fileName;
            }
            else
            {
                // fallback
                category.ImageUrl = "/images/default.jpg";
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE CATEGORY
        // =========================

        // GET
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();

            var category = await _context.Categories
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UserId == userId &&
                    !c.IsGlobal); // 🔥 нельзя удалить системные

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();

            var category = await _context.Categories
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UserId == userId &&
                    !c.IsGlobal);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}