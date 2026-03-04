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
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToList();

            return View(products);
        }

        // =========================
        // Add Product (GET)
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
        // Add Product (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                // возвращаемся обратно в категорию
                return RedirectToAction(
                    "Details",
                    "Categories",
                    new { id = product.CategoryId }
                );
            }

            return View(product);
        }

        // =========================
        // Product Details
        // =========================
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // Edit (GET)
        // =========================
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // Edit (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();

                return RedirectToAction(
                    "Details",
                    "Categories",
                    new { id = product.CategoryId }
                );
            }

            return View(product);
        }

        // =========================
        // Toggle Purchased
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TogglePurchased(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            product.IsPurchased = !product.IsPurchased;
            _context.SaveChanges();

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId }
            );
        }

        // =========================
        // Delete (GET)
        // =========================
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =========================
        // Delete (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                var categoryId = product.CategoryId;

                _context.Products.Remove(product);
                _context.SaveChanges();

                return RedirectToAction(
                    "Details",
                    "Categories",
                    new { id = categoryId }
                );
            }

            return RedirectToAction("Index");
        }
    }
}