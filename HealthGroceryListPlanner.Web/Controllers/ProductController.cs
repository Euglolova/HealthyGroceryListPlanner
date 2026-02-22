using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly GroceryContext _context;

        // Constructor (Dependency Injection)
        public ProductController(GroceryContext context)
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
                .ToList();
            return View(products);
        }

        // =========================
        // Add Product Screen (GET)
        // =========================
        public IActionResult Create()
        {
                ViewBag.Categories = _context.Categories.ToList();
                return View();
        }

        // =========================
        // Add Product Screen (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }
        // =========================
        // Product Details Screen
        // =========================
        public IActionResult Details(int id)
        {
            var product = _context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
            // =========================
            // Edit Product (GET)
            // =========================
        }
        public IActionResult Edit(int id)
        {
        var product = _context.Products.Find(id);

            if (product == null)
            {   
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // =========================
        // Edit Product (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }
        // =========================
        // Delete Product (GET)
        // =========================
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // =========================
        // Delete Product (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
    }
}
