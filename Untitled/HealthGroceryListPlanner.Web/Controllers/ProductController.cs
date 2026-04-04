using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthGroceryListPlanner.Application.Services;
using HealthGroceryListPlanner.Domain.Models;
using HealthGroceryListPlanner.Domain.Enums;

namespace HealthGroceryListPlanner.Web.Controllers
{
    
    [Authorize] // 🔥 защита — только для залогиненных
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
        // PRODUCTS IN SHOPPING LIST
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
        // REMOVE FROM LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int productId)
        {
            var userId = GetUserId();

            await _productService.RemoveFromList(productId, userId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // =========================
        // UPDATE UNIT
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

            await _productService.CreateProduct(product, userId); // ✅

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId });
        }
    }
}