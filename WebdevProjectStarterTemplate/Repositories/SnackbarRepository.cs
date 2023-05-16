using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class SnackbarRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Snackbar Get(int snackbarId)
        {
            string sql = "SELECT * FROM snackbar WHERE id = @snackbarId";

            using var connection = GetConnection();
            var snackbar = connection.QuerySingle<Snackbar>(sql, new { snackbarId });
            return snackbar;
        }
        public bool Delete(int snackbarId)
        {
            string sql = @"DELETE FROM snackbar WHERE id = @snackbarId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { snackbarId });
            return numOfEffectedRows == 1;
        }

    }
}
