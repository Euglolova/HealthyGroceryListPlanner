using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;
using System.Linq;

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
            var products = _context.Products.ToList();
            return View(products);
        }

        // =========================
        // Add Product Screen (GET)
        // =========================
        public IActionResult Create()
        {
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

            return View(product);
        }
    }
}
