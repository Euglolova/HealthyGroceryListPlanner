using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Models;
using HealthGroceryListPlanner.Web.Models.Enums;

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

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 201, Name = "Carrot", Emoji="🥕", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 202, Name = "Potato", Emoji="🥔", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 203, Name = "Tomato", Emoji="🍅", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 204, Name = "Cucumber", Emoji="🥒", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 205, Name = "Onion", Emoji="🧅", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 206, Name = "Garlic", Emoji="🧄", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 207, Name = "Bell Pepper", Emoji="🫑", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 208, Name = "Broccoli", Emoji="🥦", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 209, Name = "Cauliflower", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 210, Name = "Spinach", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },

                new Product { Id = 211, Name = "Lettuce", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 212, Name = "Zucchini", Emoji="🥒", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 213, Name = "Eggplant", Emoji="🍆", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 214, Name = "Cabbage", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 215, Name = "Red Cabbage", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 216, Name = "Brussels Sprouts", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 217, Name = "Green Beans", Emoji="🫛", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 218, Name = "Peas", Emoji="🫛", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 219, Name = "Corn", Emoji="🌽", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 220, Name = "Asparagus", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },

                new Product { Id = 221, Name = "Celery", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 222, Name = "Mushrooms", Emoji="🍄", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 223, Name = "Sweet Potato", Emoji="🍠", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 224, Name = "Radish", Emoji="🥕", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 225, Name = "Beetroot", Emoji="🥕", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 226, Name = "Kale", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 227, Name = "Arugula", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 228, Name = "Leek", Emoji="🧅", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 229, Name = "Pumpkin", Emoji="🎃", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 230, Name = "Butternut Squash", Emoji="🎃", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },

                new Product { Id = 231, Name = "Turnip", Emoji="🥕", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 232, Name = "Parsnip", Emoji="🥕", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 233, Name = "Okra", Emoji="🥬", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 234, Name = "Jalapeño", Emoji="🌶️", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false },
                new Product { Id = 235, Name = "Avocado", Emoji="🥑", Quantity=1, Unit = 0, CategoryId=1, IsPurchased=false }
            );
        }
    }
}