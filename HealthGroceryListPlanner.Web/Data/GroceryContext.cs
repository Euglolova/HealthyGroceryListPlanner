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
                new Category { Id = 1, Name = "Vegetables", ImageUrl = "/images/vegetables.jpg" },
                new Category { Id = 2, Name = "Fruits", ImageUrl = "/images/fruits.jpg" },
                new Category { Id = 3, Name = "Protein", ImageUrl = "/images/protein.jpg" },
                new Category { Id = 4, Name = "Dairy", ImageUrl = "/images/dairy.jpg" },
                new Category { Id = 5, Name = "Whole Grains", ImageUrl = "/images/grains.jpg" },
                new Category { Id = 6, Name = "Nuts & Seeds", ImageUrl = "/images/nuts.jpg" },
                new Category { Id = 7, Name = "Healthy Fats", ImageUrl = "/images/fats.jpg" },
                new Category { Id = 8, Name = "Beverages", ImageUrl = "/images/beverages.jpg" }
            );
        }
    }
}