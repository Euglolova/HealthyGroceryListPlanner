using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Web.Models;
using HealthGroceryListPlanner.Web.Data;

namespace HealthGroceryListPlanner.Web.Controllers;

public class HomeController : Controller
{
    private readonly GroceryContext _context;

    public HomeController(GroceryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Settings()
    {
        return View();
    }

    public IActionResult Planner()
    {
        return View();
    }

    // 🔥 Clear All Products
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