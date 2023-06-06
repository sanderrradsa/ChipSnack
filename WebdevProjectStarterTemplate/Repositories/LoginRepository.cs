using Dapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    [Authorize]
    public class LoginRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }


        public User Get(string naam, string wachtwoord)
        {
            string sql = "SELECT COUNT(*) FROM gebruiker WHERE email = @Username AND wachtwoord = @Password";

            using var connection = GetConnection();
            var gebruiker = connection.QuerySingle<User>(sql, new { naam, wachtwoord });
            return gebruiker;
        }

    }
}
