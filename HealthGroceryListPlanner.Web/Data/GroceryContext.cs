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
    }
}