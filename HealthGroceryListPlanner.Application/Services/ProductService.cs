using HealthGroceryListPlanner.Domain.Models;
using HealthGroceryListPlanner.Domain.Enums;
using HealthGroceryListPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthGroceryListPlanner.Application.Services
{
    public class ProductService
    {
        private readonly GroceryContext _context;

        public ProductService(GroceryContext context)
        {
            _context = context;
        }

        // =========================
        // PRODUCTS IN LIST
        // =========================
        public async Task<List<Product>> GetProductsByList(int listId, int userId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ShoppingListId == listId && p.UserId == userId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        // =========================
        // INCREASE
        // =========================
        public async Task IncreaseQuantity(int productId, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.UserId == userId);

            if (product != null)
            {
                product.Quantity++;
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // DECREASE
        // =========================
        public async Task DecreaseQuantity(int productId, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.UserId == userId);

            if (product != null && product.Quantity > 1)
            {
                product.Quantity--;
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // ADD TO LIST 
        // =========================
        public async Task AddToList(int productId, int listId, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p =>
                    p.Id == productId &&
                    (p.UserId == userId || p.IsGlobal));

            if (product == null)
                return;

           
            if (product.IsGlobal)
            {
                var userProduct = new Product
                {
                    Name = product.Name,
                    Emoji = product.Emoji,
                    CategoryId = product.CategoryId,
                    Quantity = 1,
                    Unit = product.Unit,
                    IsPurchased = false,
                    ShoppingListId = listId,
                    UserId = userId,
                    IsGlobal = false
                };

                _context.Products.Add(userProduct);
            }
            else
            {
                product.ShoppingListId = listId;
            }

            await _context.SaveChangesAsync();
        }

        // =========================
        // REMOVE
        // =========================
        public async Task RemoveFromList(int productId, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.UserId == userId);

            if (product != null)
            {
                product.ShoppingListId = null;
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // UPDATE UNIT
        // =========================
        public async Task UpdateUnit(int productId, UnitType unit, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.UserId == userId);

            if (product != null)
            {
                product.Unit = unit;
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // CATEGORIES (🔥 ФИЛЬТР)
        // =========================
        public async Task<List<Category>> GetCategoriesWithProducts(int userId)
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.IsGlobal || c.UserId == userId) // 🔥 ВОТ ГЛАВНОЕ
                .OrderBy(c => c.Name)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.Products = category.Products
                    .Where(p => p.UserId == userId || p.IsGlobal)
                    .ToList();
            }

            return categories;
        }

        // =========================
        // CREATE PRODUCT
        // =========================
        public async Task CreateProduct(Product product, int userId)
        {
            product.UserId = userId;
            product.IsGlobal = false;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}