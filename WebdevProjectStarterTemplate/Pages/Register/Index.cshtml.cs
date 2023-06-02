using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using static Dapper.SqlMapper;

namespace WebdevProjectStarterTemplate.Pages.Register
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public string pagename = "Index";
        public void OnGet()
        {
            string connectionString = "Server=127.0.0.1;Port=3306;Database=WebdevProject;Uid=root;";
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ";
                //string
                var identity = CookieAuthenticationDefaults.AuthenticationScheme;
                //ide

                //var principal = new ClaimsPrincipal(identity);

                /*var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Set the cookie to be persistent
                    ExpiresUtc = DateTime.UtcNow.AddMonths(1) // Set the expiration date of the cookie


                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                */
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string naam, string email,int microsoftId, string password, int admin)
        {

            string connectionString = "Server=127.0.0.1;Port=3306;Database=WebdevProject;Uid=root;";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO gebruiker VALUES (UUID(), @Naam, @Email, @MicrosoftId, @Password, @Admin)";
                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Naam", naam);
                    command.Parameters.AddWithValue("@MicrosoftId", microsoftId);
                    command.Parameters.AddWithValue("@Admin", admin);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    
                }
                
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                connection.Close();
                return RedirectToPage("Registered");
            }
            
            //return Page();




        }
    }
}

