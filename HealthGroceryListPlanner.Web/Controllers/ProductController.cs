using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Application.Services;
using HealthGroceryListPlanner.Domain.Models;
using HealthGroceryListPlanner.Domain.Enums;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // =========================
        // PRODUCTS IN SHOPPING LIST
        // =========================
        public async Task<IActionResult> List(int id)
        {
            var products = await _productService.GetProductsByList(id);

            ViewBag.ShoppingListId = id;

            return View(products);
        }

        // =========================
        // SELECT PRODUCTS FOR LIST
        // =========================
        public async Task<IActionResult> SelectForList(int shoppingListId)
        {
            var categories = await _productService.GetCategoriesWithProducts();

            ViewBag.ShoppingListId = shoppingListId;

            return View(categories);
        }

        // =========================
        // ADD PRODUCT TO LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> AddToList(int productId, int shoppingListId)
        {
            await _productService.AddToList(productId, shoppingListId);

            return RedirectToAction("List", new { id = shoppingListId });
        }

        // =========================
        // CHANGE QUANTITY
        // =========================
        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            await _productService.IncreaseQuantity(productId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            await _productService.DecreaseQuantity(productId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // =========================
        // REMOVE FROM LIST
        // =========================
        [HttpPost]
        public async Task<IActionResult> RemoveFromList(int productId)
        {
            await _productService.RemoveFromList(productId);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // =========================
        // UPDATE UNIT
        // =========================
        [HttpPost]
        public async Task<IActionResult> UpdateUnit(int productId, UnitType unit)
        {
            await _productService.UpdateUnit(productId, unit);

            return Redirect(Request.Headers["Referer"].ToString());
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

            await _productService.CreateProduct(product);

            return RedirectToAction(
                "Details",
                "Categories",
                new { id = product.CategoryId });
        }
    }
}