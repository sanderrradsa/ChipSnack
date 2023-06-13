using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Threading.Tasks;
using WebdevProjectStarterTemplate.Pages.Shared;

namespace WebdevProjectStarterTemplate.Pages
{


    public class AccountController : Controller
    {

        IHttpContextAccessor httpContextAccessor;
        Pages_Shared__LoggedInUser user;
        public bool IsValidUser(string username, string password)
        {
            return username != null || password != null;

        }
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
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
        [HttpGet]
        public int GetID(string username)
        {
            int ReaderInt = 0;
            
            try
            {
                

                string connectionString = GetConnection().ConnectionString;
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id FROM gebruiker WHERE email = @Username;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ReaderInt = reader.GetInt32(reader.GetOrdinal("id"));
                        }




                        int count = Convert.ToInt32(command.ExecuteScalar());
                        string query1 = "SELECT * FROM gebruiker where email = @Username and rol = 1";
                    }
                }
                return ReaderInt;
            }
            catch
            {
                ///
                return ReaderInt;
            }
            
            

        }
        [HttpGet]
        public bool IsAdmin(string username)
        {
            var ReaderInt = 0;

            try
            {


                string connectionString = GetConnection().ConnectionString;
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT rol FROM gebruiker WHERE email = @Username;";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ReaderInt = reader.GetInt32("rol");
                        }




                        int count = Convert.ToInt32(command.ExecuteScalar());
                        string query1 = "SELECT * FROM gebruiker where email = @Username and rol = 1";
                    }
                }
                if (ReaderInt == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                ///
                if (ReaderInt == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }



        }
    }
}
