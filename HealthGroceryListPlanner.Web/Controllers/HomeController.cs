using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Infrastructure.Data;
using HealthGroceryListPlanner.Domain.Models;
using System.Diagnostics;

namespace HealthGroceryListPlanner.Web.Controllers;

public class HomeController : Controller
{
    private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }
    private readonly GroceryContext _context;

    public HomeController(GroceryContext context)
    {
        _context = context;
    }

   [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

        public IActionResult Settings()
        {
            return RedirectToAction("Index", "Settings");
        }

    public IActionResult Planner()
    {
        return View();
    }

    // Clear All Products
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ClearAllProducts()
    {
        var products = _context.Products.ToList();

        if (products.Any())
        {
            _context.Products.RemoveRange(products);
            _context.SaveChanges();
        }

        TempData["Message"] = "All products have been deleted.";
        return RedirectToAction("Settings");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        });
    }
}