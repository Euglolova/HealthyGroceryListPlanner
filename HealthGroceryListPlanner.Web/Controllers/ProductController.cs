using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly GroceryContext _context;

        public ProductsController(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // Grocery List Screen
        // =========================
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return View(products);
        }

        // =========================
        // CREATE PRODUCT (GET)
        // =========================
        public IActionResult Create(int categoryId)
        {
            var product = new Product
            {
                CategoryId = categoryId
            };

            return View(product);
        }

        // =========================
        // CREATE PRODUCT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
           if (!ModelState.IsValid)
               return View(product);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId }
            );
        }

        // =========================
        // PRODUCT DETAILS
        // =========================
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // EDIT PRODUCT (GET)
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // EDIT PRODUCT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId }
            );
        }

        // =========================
        // DELETE PRODUCT (GET)
        // =========================
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // DELETE PRODUCT (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return RedirectToAction("Index");

            var categoryId = product.CategoryId;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = categoryId }
            );
        }
    }
}