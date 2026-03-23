using Microsoft.AspNetCore.Mvc;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}