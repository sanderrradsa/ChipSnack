using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebdevProjectStarterTemplate.Pages
{


    public class AccountController : Controller
    {
        public bool IsValidUser(string username, string password)
        {
            return username != null|| password != null;
        }
        // Login action
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Check username and password
            if (IsValidUser(username, password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
                // Add any additional claims you need
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home"); // Redirect to desired page after login
            }

            // Invalid credentials
            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }
    }
}
