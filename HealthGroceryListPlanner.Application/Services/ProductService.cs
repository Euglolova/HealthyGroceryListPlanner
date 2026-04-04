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
        // GET PRODUCTS IN LIST
        // =========================
        public async Task<List<Product>> GetProductsByList(int listId, int userId)
        {
            // Get hidden products for current user
            var hiddenIds = await _context.UserHiddenProducts
                .Where(h => h.UserId == userId)
                .Select(h => h.ProductId)
                .ToListAsync();

            // Get ONLY cart products (NOT global)
            return await _context.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.ShoppingListId == listId && // this list
                    p.UserId == userId &&         // this user
                    !p.IsGlobal &&               // ONLY cart items
                    !hiddenIds.Contains(p.Id))   // exclude hidden
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        // =========================
        // GET PRODUCT BY ID (NO USER FILTER)
        // =========================
        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // =========================
        // DELETE USER PRODUCT ONLY
        // =========================
        public async Task DeleteProduct(int id, int userId)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // HIDE GLOBAL PRODUCT FOR USER
        // =========================
        public async Task HideProduct(int productId, int userId)
        {
            var exists = await _context.UserHiddenProducts
                .AnyAsync(h => h.ProductId == productId && h.UserId == userId);

            if (!exists)
            {
                _context.UserHiddenProducts.Add(new UserHiddenProduct
                {
                    ProductId = productId,
                    UserId = userId
                });

                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // INCREASE QUANTITY
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
        // DECREASE QUANTITY
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
        // ADD PRODUCT TO LIST
        // =========================
       public async Task AddToList(int productId, int listId, int userId)
        {
            // 1. Get product from catalog (ONLY global or user base product)
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.ShoppingListId == null);

            if (product == null)
                return;

            // 2. Check ONLY inside shopping list (not global!)
            var existing = await _context.Products
                .FirstOrDefaultAsync(p =>
                    p.Name == product.Name &&
                    p.CategoryId == product.CategoryId &&
                    p.ShoppingListId == listId &&
                    p.UserId == userId &&
                    !p.IsGlobal); // 🔥 ОЧЕНЬ ВАЖНО

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                // 3. Create NEW copy (cart item)
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

            await _context.SaveChangesAsync();
        }
        // =========================
        // REMOVE FROM LIST
        // =========================
        public async Task RemoveFromList(int productId, int userId)
        {
            // Find ONLY user cart product (not global)
            var product = await _context.Products
                .FirstOrDefaultAsync(p =>
                    p.Id == productId &&
                    p.UserId == userId &&
                    !p.IsGlobal);

            if (product != null)
            {
                // Remove from database (cart item)
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // =========================
        // UPDATE UNIT TYPE
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
        // GET CATEGORIES WITH PRODUCTS
        // =========================
        public async Task<List<Category>> GetCategoriesWithProducts(int userId)
        {
            var hiddenIds = await _context.UserHiddenProducts
                .Where(h => h.UserId == userId)
                .Select(h => h.ProductId)
                .ToListAsync();

            var categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.IsGlobal || c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.Products = category.Products
                    .Where(p =>
                        (p.UserId == userId || p.IsGlobal) &&
                        p.ShoppingListId == null &&        // exclude cart items
                        !hiddenIds.Contains(p.Id))
                    
                    // remove duplicates (user priority)
                    .GroupBy(p => p.Name)
                    .Select(g =>
                        g.FirstOrDefault(p => p.UserId == userId) // user first
                        ?? g.First()) // otherwise global
                    
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