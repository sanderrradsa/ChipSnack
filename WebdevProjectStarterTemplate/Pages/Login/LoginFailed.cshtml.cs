using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
namespace WebdevProjectStarterTemplate.Pages.Login
{

    public class LoginFailed : PageModel
    {
        

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {

            string connectionString = "Server=127.0.0.1;Port=3306;Database=WebdevProject;Uid=root;";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM gebruiker WHERE email = @Username AND wachtwoord = @Password";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 1)
                    {

                        // Login successful, redirect to a different page

                        var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, username)
                        // Add any additional claims you need
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Set the cookie to be persistent
                            ExpiresUtc = DateTime.UtcNow.AddMonths(1) // Set the expiration date of the cookie


                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                        //return RedirectToPage("/Index");

                        return RedirectToPage("/Privacy");
                    }
                    else
                    {
                        // Login failed, display an error message
                        //ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return RedirectToPage("/Login/LoginFailed");
                    }
                }
            }
        }
    }
}