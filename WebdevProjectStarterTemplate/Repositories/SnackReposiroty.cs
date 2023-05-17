using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class SnackReposiroty
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Snack Get(int snackId)
        {
            string sql = "SELECT * FROM snack WHERE id = @snackId";

            using var connection = GetConnection();
            var snack = connection.QuerySingle<Snack>(sql, new { snackId });
            return snack;
        }
        public bool Delete(int snackId)
        {
            string sql = @"DELETE FROM snack WHERE id = @snackId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { snackId });
            return numOfEffectedRows == 1;
        }

    }
}
