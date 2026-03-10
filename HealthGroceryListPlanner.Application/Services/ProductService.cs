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

        public async Task<List<Product>> GetProductsByList(int listId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.ShoppingListId == listId)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task IncreaseQuantity(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                product.Quantity++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecreaseQuantity(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null && product.Quantity > 1)
            {
                product.Quantity--;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddToList(int productId, int listId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                product.ShoppingListId = listId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromList(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                product.ShoppingListId = null;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUnit(int productId, UnitType unit)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product != null)
            {
                product.Unit = unit;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Category>> GetCategoriesWithProducts()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}