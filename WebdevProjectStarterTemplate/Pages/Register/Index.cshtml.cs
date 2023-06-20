using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IActionResult> OnPostAsync(string naam, string email, int microsoftId, string password, int admin)
        {

            string connectionString = GetConnection().ConnectionString;
            Random rnd = new Random();
            Int16 id = (Int16)rnd.Next(0, 999999999);
            string hashedPassword = HashPassword(password);
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO gebruiker VALUES (@id, @Naam, @Email, @Password, @Admin)";
                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Naam", naam);
                    command.Parameters.AddWithValue("@Admin", admin);
                    //command.Parameters.AddWithValue("@Admin", admin ? 1 : 0);
                    int count = Convert.ToInt32(command.ExecuteScalar());


                }

                connection.Close();
                return RedirectToPage("Registered");
            }





        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

