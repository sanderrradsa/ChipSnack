using System.Data;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class SnackbarRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }


    }
}
