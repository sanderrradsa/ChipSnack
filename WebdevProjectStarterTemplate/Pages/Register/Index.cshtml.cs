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
        // Pagina naam
        public string pagename = "Index";

        // Methode om een databaseverbinding te verkrijgen
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        // GET-handler voor de pagina
        public void OnGet()
        {
            string connectionString = GetConnection().ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var identity = CookieAuthenticationDefaults.AuthenticationScheme;
            }
        }

        // POST-handler voor het registreren van een nieuwe gebruiker
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string naam, string email, int microsoftId, string password, int admin)
        {
            string connectionString = GetConnection().ConnectionString;

            // Genereer een willekeurig ID
            Random rnd = new Random();
            Int16 id = (Int16)rnd.Next(0, 999999999);

            // Hash het wachtwoord
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
                // Loop door elke byte in de hash en voeg deze toe aan de string builder
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Converteer byte naar hexadecimale string
                }
                // Geef de resulterende gehashte wachtwoordstring terug
                return builder.ToString();
            }
        }

    }
}

