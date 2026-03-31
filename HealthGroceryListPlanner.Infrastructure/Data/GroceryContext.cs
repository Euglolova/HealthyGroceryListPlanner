using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Domain.Models;

namespace HealthGroceryListPlanner.Infrastructure.Data
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // USER
            // =========================
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            // =========================
            // RELATIONS
            // =========================

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ShoppingList)
                .WithMany(l => l.Products)
                .HasForeignKey(p => p.ShoppingListId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ShoppingList>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // =========================
            // CATEGORY SEED (🔥 FIXED)
            // =========================
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Vegetables", ImageUrl = "/images/vegetables.jpg", IsGlobal = true },
                new Category { Id = 2, Name = "Fruits and Berries", ImageUrl = "/images/fruits.jpg", IsGlobal = true },
                new Category { Id = 3, Name = "Protein", ImageUrl = "/images/protein.jpg", IsGlobal = true },
                new Category { Id = 4, Name = "Dairy", ImageUrl = "/images/dairy.jpg", IsGlobal = true },
                new Category { Id = 5, Name = "Whole Grains", ImageUrl = "/images/grains.jpg", IsGlobal = true },
                new Category { Id = 6, Name = "Nuts & Seeds", ImageUrl = "/images/nuts.jpg", IsGlobal = true },
                new Category { Id = 7, Name = "Healthy Fats", ImageUrl = "/images/fats.jpg", IsGlobal = true },
                new Category { Id = 8, Name = "Beverages", ImageUrl = "/images/beverages.jpg", IsGlobal = true }
            );

            // =========================
            // PRODUCT SEED (🔥 FIXED)
            // =========================
            modelBuilder.Entity<Product>().HasData(

                // Vegetables
                new Product { Id = 201, Name = "Carrot", Emoji="🥕", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false, IsGlobal=true },
                new Product { Id = 202, Name = "Potato", Emoji="🥔", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false, IsGlobal=true },
                new Product { Id = 203, Name = "Tomato", Emoji="🍅", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false, IsGlobal=true },

                // Fruits
                new Product { Id = 301, Name = "Apple", Emoji="🍎", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false, IsGlobal=true },
                new Product { Id = 302, Name = "Banana", Emoji="🍌", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false, IsGlobal=true },

                // Protein
                new Product { Id = 401, Name = "Chicken Breast", Emoji="🍗", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false, IsGlobal=true },
                new Product { Id = 402, Name = "Salmon", Emoji="🐟", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false, IsGlobal=true },

                // Dairy
                new Product { Id = 501, Name = "Milk", Emoji="🥛", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false, IsGlobal=true },

                // Grains
                new Product { Id = 601, Name = "Oats", Emoji="🥣", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false, IsGlobal=true },

                // Nuts
                new Product { Id = 701, Name = "Almonds", Emoji="🌰", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false, IsGlobal=true },

                // Fats
                new Product { Id = 801, Name = "Avocado", Emoji="🥑", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false, IsGlobal=true },

                // Drinks
                new Product { Id = 901, Name = "Coffee", Emoji="☕", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false, IsGlobal=true }
            );
        }
    }
}