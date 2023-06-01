using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class BudgetRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }
        public IEnumerable<Budget> Get()
        {
            string sql = "SELECT * FROM budget";

            using var connection = GetConnection();
            var budget = connection.Query<Budget>(sql);
            return budget;
        }

        public Budget Get(int budgetId)
        {
            string sql = "SELECT * FROM budget WHERE id = @budgetId";

            using var connection = GetConnection();
            var budget = connection.QuerySingle<Budget>(sql, new { budgetId });
            return budget;
        }
        public Budget Update(Budget budget)
        {
            string sql = @"
                UPDATE budget SET 
                    budgetMax = @BudgetMax 
                WHERE id = @Id;
                SELECT * FROM budget WHERE id = @Id";

            using var connection = GetConnection();
            var updatedBudget = connection.QuerySingle<Budget>(sql, budget);
            return updatedBudget;
        }
    }
}
