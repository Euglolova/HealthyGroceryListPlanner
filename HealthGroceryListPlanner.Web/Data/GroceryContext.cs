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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Vegetables" },
                new Category { Id = 2, Name = "Fruits" },
                new Category { Id = 3, Name = "Protein" },
                new Category { Id = 4, Name = "Dairy" },
                new Category { Id = 5, Name = "Whole Grains" },
                new Category { Id = 6, Name = "Nuts & Seeds" },
                new Category { Id = 7, Name = "Healthy Fats" },
                new Category { Id = 8, Name = "Beverages" }
            );
        }
    }
}