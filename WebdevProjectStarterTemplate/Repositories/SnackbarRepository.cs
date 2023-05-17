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

        public IEnumerable<Snackbar> Get()
        {
            string sql = "SELECT * FROM snackbar";

            using var connection = GetConnection();
            var snackbars = connection.Query<Snackbar>(sql);
            return snackbars;
        }
        public bool Delete(int snackbarId)
        {
            string sql = @"DELETE FROM snackbar WHERE id = @snackbarId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { snackbarId });
            return numOfEffectedRows == 1;
        }

        public Snackbar Update(Snackbar snackbar)
        {
            string sql = @"
                UPDATE snackbar SET 
                    naam = @naam 
                WHERE id = @id;
                SELECT * FROM snackbar WHERE id = @id";

            using var connection = GetConnection();
            var updatedSnackbar = connection.QuerySingle<Snackbar>(sql, snackbar);
            return updatedSnackbar;
        }
        
        

    }
}
