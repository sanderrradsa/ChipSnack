using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Dapper;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebdevProjectStarterTemplate.Pages.WachtwoordVergeten
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            try { 

                bool doesExist;
                string connectionString = GetConnection().ConnectionString;
                
                    string hashedPassword = HashPassword(password);

                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query1 = "SELECT * FROM gebruiker where email = @Username";
                        using (var command1 = new MySqlCommand(@query1, connection))
                        {
                            command1.Parameters.AddWithValue("@Username", username);
                            MySqlDataReader reader = command1.ExecuteReader();
                            doesExist = reader.Read();
                            reader.Close();
                            await reader.CloseAsync();
                            connection.Close();

                        }

                        if (doesExist)
                        {
                            connection.Open();
                            string query = "UPDATE gebruiker SET wachtwoord = @password WHERE email = @Username";
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Username", username);
                                command.Parameters.AddWithValue("@Password", hashedPassword);

                                int count = Convert.ToInt32(command.ExecuteScalar());

                                return RedirectToPage("/WachtwoordVergeten/Succes");
                            }
                        }

                        else
                        {
                            // Login failed, display error page
                            return RedirectToPage("/WachtwoordVergeten/ChangeFailed");
                        }
                    }

            }
            catch {
                // Login failed, display error page
                return RedirectToPage("/WachtwoordVergeten/ChangeFailed");
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