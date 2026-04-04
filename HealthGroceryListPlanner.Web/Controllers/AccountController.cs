using Microsoft.AspNetCore.Mvc;
using HealthGroceryListPlanner.Application.Services;
using HealthGroceryListPlanner.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace HealthGroceryListPlanner.Web.Controllers
{
    public class AccountController : Controller
    {
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new Exception("User not authenticated");

            return int.Parse(claim.Value);
        }
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        // ================= LOGIN =================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _authService.Login(email, password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View();
            }

            await SignIn(user);

            return RedirectToAction("Index", "Home");
        }

        // ================= REGISTER =================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            var user = await _authService.Register(name, email, password);

            if (user == null)
            {
                ModelState.AddModelError("email", "This email is already registered");
                return View();
            }

            await SignIn(user);

            return RedirectToAction("Index", "Home");
        }

        // ================= LOGOUT =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        // ================= SIGN IN =================
        private async Task SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    // ================= ACCESS DENIED =================
        public IActionResult AccessDenied()
        {
            return View();
        }    
    }
}