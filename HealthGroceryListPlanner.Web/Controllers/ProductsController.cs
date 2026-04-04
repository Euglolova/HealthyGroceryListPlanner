using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Application.Services;
using HealthGroceryListPlanner.Domain.Models;
using HealthGroceryListPlanner.Domain.Enums;

namespace HealthGroceryListPlanner.Web.Controllers
{
    // 🔒 Only authenticated users can access products
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // =========================
        // GET CURRENT USER ID
        // =========================
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null || string.IsNullOrEmpty(claim.Value))
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }

        // =========================
        // GET PRODUCTS IN SHOPPING LIST
        // =========================
        public async Task<IActionResult> List(int id)
        {
            var userId = GetUserId();

            var products = await _productService.GetProductsByList(id, userId);

            ViewBag.ShoppingListId = id;

            return View(products);
        }

        // =========================
        // SELECT PRODUCTS FOR LIST
        // =========================
        public async Task<IActionResult> SelectForList(int shoppingListId)
        {
            var userId = GetUserId();

            var categories = await _productService.GetCategoriesWithProducts(userId);

            ViewBag.ShoppingListId = shoppingListId;

            return View(categories);
        }

        // =========================
        // ADD PRODUCT TO LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> AddToList(int productId, int shoppingListId)
        {
            var userId = GetUserId();

            await _productService.AddToList(productId, shoppingListId, userId);

            return RedirectToAction("List", new { id = shoppingListId });
        }

        // =========================
        // CHANGE QUANTITY
        // =========================
        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            var userId = GetUserId();

            await _productService.IncreaseQuantity(productId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            var userId = GetUserId();

            await _productService.DecreaseQuantity(productId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // =========================
        // REMOVE PRODUCT FROM LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int productId)
        {
            var userId = GetUserId();

            await _productService.RemoveFromList(productId, userId);

            return RedirectToAction("List", new { id = productId });
        }

        // =========================
        // UPDATE UNIT TYPE
        // =========================
        [HttpPost]
        public async Task<IActionResult> UpdateUnit(int productId, UnitType unit)
        {
            var userId = GetUserId();

            await _productService.UpdateUnit(productId, unit, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // =========================
        // CREATE PRODUCT (GET)
        // =========================
        public IActionResult Create(int categoryId)
        {
            return View(new Product { CategoryId = categoryId });
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

            var userId = GetUserId();

            await _productService.CreateProduct(product, userId);

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId });
        }

        // =========================
        // DELETE OR HIDE PRODUCT
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Get current logged-in user ID from claims
            var userId = GetUserId();

            // Get product by ID (without strict user filtering,
            // because we need access to global products too)
            var product = await _productService.GetProductById(id);

            // If product does not exist → return 404
            if (product == null)
                return NotFound();

            // =========================
            // GLOBAL PRODUCT (SEED DATA)
            // =========================
            if (product.IsGlobal)
            {
                // Do NOT delete from database
                // Just hide it for this specific user
                await _productService.HideProduct(id, userId);
            }
            else
            {
                // =========================
                // USER PRODUCT
                // =========================

                // Security check: user can delete ONLY their own products
                if (product.UserId != userId)
                    return Forbid();

                // Permanently delete user's own product
                await _productService.DeleteProduct(id, userId);
            }

            // Redirect back to category details page
            return RedirectToAction("Details", "Categories", new { id = product.CategoryId });
        }
        // =========================
        // EDIT PRODUCT (GET)
        // =========================
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            var product = await _productService.GetProductById(id);

            if (product == null)
                return NotFound();

            // 🔒 user can edit ONLY own products
            if (product.UserId != userId)
                return Forbid();

            return View(product);
        }
    }
    
}