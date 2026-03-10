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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Vegetables", ImageUrl = "/images/vegetables.jpg" },
                new Category { Id = 2, Name = "Fruits and Berries", ImageUrl = "/images/fruits.jpg" },
                new Category { Id = 3, Name = "Protein", ImageUrl = "/images/protein.jpg" },
                new Category { Id = 4, Name = "Dairy", ImageUrl = "/images/dairy.jpg" },
                new Category { Id = 5, Name = "Whole Grains", ImageUrl = "/images/grains.jpg" },
                new Category { Id = 6, Name = "Nuts & Seeds", ImageUrl = "/images/nuts.jpg" },
                new Category { Id = 7, Name = "Healthy Fats", ImageUrl = "/images/fats.jpg" },
                new Category { Id = 8, Name = "Beverages", ImageUrl = "/images/beverages.jpg" }
            );

                    modelBuilder.Entity<Product>().HasData(

            // =========================
           
            new Product { Id = 201, Name = "Carrot", Emoji="🥕", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 202, Name = "Potato", Emoji="🥔", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 203, Name = "Tomato", Emoji="🍅", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 204, Name = "Cucumber", Emoji="🥒", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 205, Name = "Onion", Emoji="🧅", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 206, Name = "Garlic", Emoji="🧄", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 207, Name = "Eggplant", Emoji="🍆", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },
            new Product { Id = 208, Name = "Corn", Emoji="🌽", Quantity=1, Unit=0, CategoryId=1, IsPurchased=false },

            // =========================
            // Fruits (CategoryId = 2)
      
            new Product { Id = 301, Name = "Apple", Emoji="🍎", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false },
            new Product { Id = 302, Name = "Banana", Emoji="🍌", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false },
            new Product { Id = 303, Name = "Orange", Emoji="🍊", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false },
            new Product { Id = 304, Name = "Strawberry", Emoji="🍓", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false },
            new Product { Id = 305, Name = "Grapes", Emoji="🍇", Quantity=1, Unit=0, CategoryId=2, IsPurchased=false },

            // =========================
            // Protein (CategoryId = 3)
            
            new Product { Id = 401, Name = "Chicken Breast", Emoji="🍗", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false },
            new Product { Id = 402, Name = "Salmon", Emoji="🐟", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false },
            new Product { Id = 403, Name = "Eggs", Emoji="🥚", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false },
            new Product { Id = 404, Name = "Tofu", Emoji="🧊", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false },
            new Product { Id = 405, Name = "Beans", Emoji="🫘", Quantity=1, Unit=0, CategoryId=3, IsPurchased=false },

            // =========================
            // Dairy (CategoryId = 4)
    
            new Product { Id = 501, Name = "Milk", Emoji="🥛", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false },
            new Product { Id = 502, Name = "Cheese", Emoji="🧀", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false },
            new Product { Id = 503, Name = "Yogurt", Emoji="🥣", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false },
            new Product { Id = 504, Name = "Butter", Emoji="🧈", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false },
            new Product { Id = 505, Name = "Cottage Cheese", Emoji="🥛", Quantity=1, Unit=0, CategoryId=4, IsPurchased=false },

            // =========================
            // Whole Grains (CategoryId = 5)
           
            new Product { Id = 601, Name = "Brown Rice", Emoji="🍚", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false },
            new Product { Id = 602, Name = "Oats", Emoji="🥣", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false },
            new Product { Id = 603, Name = "Quinoa", Emoji="🍚", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false },
            new Product { Id = 604, Name = "Whole Wheat Bread", Emoji="🍞", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false },
            new Product { Id = 605, Name = "Barley", Emoji="🌾", Quantity=1, Unit=0, CategoryId=5, IsPurchased=false },

            // =========================
            // Nuts & Seeds (CategoryId = 6)
   
            new Product { Id = 701, Name = "Almonds", Emoji="🌰", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false },
            new Product { Id = 702, Name = "Walnuts", Emoji="🌰", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false },
            new Product { Id = 703, Name = "Cashews", Emoji="🥜", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false },
            new Product { Id = 704, Name = "Chia Seeds", Emoji="🌱", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false },
            new Product { Id = 705, Name = "Flax Seeds", Emoji="🌱", Quantity=1, Unit=0, CategoryId=6, IsPurchased=false },

            // =========================
            // Healthy Fats (CategoryId = 7)

            new Product { Id = 801, Name = "Avocado", Emoji="🥑", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false },
            new Product { Id = 802, Name = "Olive Oil", Emoji="🫒", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false },
            new Product { Id = 803, Name = "Coconut Oil", Emoji="🥥", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false },
            new Product { Id = 804, Name = "Dark Chocolate", Emoji="🍫", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false },
            new Product { Id = 805, Name = "Peanut Butter", Emoji="🥜", Quantity=1, Unit=0, CategoryId=7, IsPurchased=false },

            // =========================
            // Beverages (CategoryId = 8)
           
            new Product { Id = 901, Name = "Coffee", Emoji="☕", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false },
            new Product { Id = 902, Name = "Green Tea", Emoji="🍵", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false },
            new Product { Id = 903, Name = "Black Tea", Emoji="🍵", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false },
            new Product { Id = 904, Name = "Orange Juice", Emoji="🍊", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false },
            new Product { Id = 905, Name = "Smoothie", Emoji="🥤", Quantity=1, Unit=0, CategoryId=8, IsPurchased=false }

        );

        }
    }
}