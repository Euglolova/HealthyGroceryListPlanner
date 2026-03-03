using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthGroceryListPlanner.Web.Data;
using HealthGroceryListPlanner.Web.Models;
using System.Linq;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly GroceryContext _context;

        public CategoriesController(GroceryContext context)
        {
            _context = context;
        }

        // GET: /Categories
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: /Categories/Details/5
        public IActionResult Details(int id)
        {
            var category = _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}