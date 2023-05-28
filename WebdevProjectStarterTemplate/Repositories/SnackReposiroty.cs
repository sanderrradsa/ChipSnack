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

        public IEnumerable<Snack> Get()
        {
            string sql = "SELECT * FROM snack";

            using var connection = GetConnection();
            var snacks = connection.Query<Snack>(sql);
            return snacks;
        }

        public IEnumerable<Snack> GetFromSnackBar(int snackbarId)
        {
            string sql = @"SELECT * FROM snack WHERE snackbarId = @snackbarId";

            using var connection = GetConnection();
            var snacks = connection.Query<Snack>(sql, new { snackbarId });
            return snacks;
        }

        // Van snackbar obj of van id? welke is beter?
        public IEnumerable<Snack> GetFromSnackBar(Snackbar snackbar)
        {
            return GetFromSnackBar(snackbar.Id);
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
