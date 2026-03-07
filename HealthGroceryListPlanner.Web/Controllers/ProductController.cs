using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;
using HealthGroceryListPlanner.Web.Models.Enums;

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
        // ALL PRODUCTS
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
        // PRODUCTS IN SHOPPING LIST
        // =========================
        public async Task<IActionResult> List(int id)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ShoppingListId == id)
                .OrderBy(p => p.Name)
                .ToListAsync();

            ViewBag.ShoppingListId = id;

            return View(products);
        }

        // =========================
        // SELECT PRODUCTS FOR LIST
        // =========================
        public async Task<IActionResult> SelectForList(int shoppingListId)
        {
            ViewBag.ShoppingListId = shoppingListId;

            var categories = await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return View(categories);
        }

        // =========================
        // ADD PRODUCT TO SHOPPING LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> AddToList(int productId, int shoppingListId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return NotFound();

            product.ShoppingListId = shoppingListId;

            await _context.SaveChangesAsync();

            return RedirectToAction("List", new { id = shoppingListId });
        }

        // =========================
        // CREATE PRODUCT
        // =========================
        public IActionResult Create(int categoryId)
        {
            return View(new Product { CategoryId = categoryId });
        }

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
                new { id = product.CategoryId });
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
        // EDIT PRODUCT
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

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
                new { id = product.CategoryId });
        }

        // =========================
        // DELETE PRODUCT
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
                new { id = categoryId });
        }

        // =========================
        // CHANGE QUANTITY
        // =========================
        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return NotFound();

            product.Quantity = (product.Quantity ?? 0) + 1;

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return NotFound();

            if ((product.Quantity ?? 1) > 1)
                product.Quantity--;

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return NotFound();

            var listId = product.ShoppingListId;

            product.ShoppingListId = null;

            await _context.SaveChangesAsync();

            return RedirectToAction("List", new { id = listId });
        }
        
            [HttpPost]
            public async Task<IActionResult> UpdateUnit(int productId, UnitType unit)
            {
                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                    return NotFound();

                product.Unit = unit;

                await _context.SaveChangesAsync();

                return Redirect(Request.Headers["Referer"].ToString());
            }
        
    }
}