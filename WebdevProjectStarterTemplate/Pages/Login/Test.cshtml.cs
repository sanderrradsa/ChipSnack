using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebdevProjectStarterTemplate.Pages.Login
{

    public class Test : PageModel
    {

        public int ReaderInt;
        public string username;

        

        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public void OnGet()
        {


            username = HttpContext.User.Identity.Name;
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
        }
    }
}
