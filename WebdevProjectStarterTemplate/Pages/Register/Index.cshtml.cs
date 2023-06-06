using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using static Dapper.SqlMapper;
using System.Data;

namespace WebdevProjectStarterTemplate.Pages.Register
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {

        public string pagename = "Index";
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }


        public void OnGet()
        {
            string connectionString = GetConnection().ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var identity = CookieAuthenticationDefaults.AuthenticationScheme;
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string naam, string email,int microsoftId, string password, int admin)
        {

            string connectionString = GetConnection().ConnectionString;

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
                
                connection.Close();
                return RedirectToPage("Registered");
            }
            




        }
    }
}

