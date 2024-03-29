using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using System.Data;

namespace WebdevProjectStarterTemplate.Pages.Login
{

    public class LoginFailed : PageModel
    {

        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {

            string connectionString = GetConnection().ConnectionString;

            string hashedPassword = HashPassword(password);
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM gebruiker WHERE email = @Username AND wachtwoord = @Password";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    string query1 = "SELECT * FROM gebruiker where email = @Username and rol = 1";
                    using (var command1 = new MySqlCommand(@query1, connection))
                    {
                        command1.Parameters.AddWithValue("@Username", username);
                        MySqlDataReader reader = command1.ExecuteReader();
                        bool admin = reader.Read();


                        if (count == 1 && admin)
                        {

                            // Inloggen is gelukt, doorsturen naar een andere pagina

                            var claims = new List<Claim>{
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, "admin")
        // Voeg eventuele extra claims toe die nodig zijn
    };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true, // Zet de cookie als persistent
                                ExpiresUtc = DateTime.UtcNow.AddMonths(1) // Stel de vervaldatum van de cookie in
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                            return RedirectToPage("/Index");
                        }
                        else if (count == 1 && !admin)
                        {

                            // Inloggen is gelukt, doorsturen naar een andere pagina

                            var claims = new List<Claim>{
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, "gebruiker")
                            // Voeg eventuele extra claims toe die nodig zijn
                            };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);

                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true, // Zet de cookie als persistent
                                ExpiresUtc = DateTime.UtcNow.AddMonths(1) // Stel de vervaldatum van de cookie in
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                            //return RedirectToPage("/Index");

                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            // Inloggen is mislukt, toon foutpagina
                            return RedirectToPage("/Login/LoginFailed");
                        }

                    }
                }
            }

        }
        // Methode voor het hashen van het wachtwoord
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