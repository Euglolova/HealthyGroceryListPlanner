using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Models;

namespace HealthGroceryListPlanner.Web.Data
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions<GroceryContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Vegetables", ImageUrl = "/images/categories/vegetables.jpg" },
                new Category { Id = 2, Name = "Fruits", ImageUrl = "/images/categories/fruits.jpg" },
                new Category { Id = 3, Name = "Protein", ImageUrl = "/images/categories/protein.jpg" },
                new Category { Id = 4, Name = "Dairy", ImageUrl = "/images/categories/dairy.jpg" },
                new Category { Id = 5, Name = "Whole Grains", ImageUrl = "/images/categories/grains.jpg" },
                new Category { Id = 6, Name = "Nuts & Seeds", ImageUrl = "/images/categories/nuts.jpg" },
                new Category { Id = 7, Name = "Healthy Fats", ImageUrl = "/images/categories/fats.jpg" },
                new Category { Id = 8, Name = "Beverages", ImageUrl = "/images/categories/beverages.jpg" }
            );
        }
    }
}