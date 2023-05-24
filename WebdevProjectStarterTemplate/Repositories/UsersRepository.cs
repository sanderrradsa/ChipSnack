using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class UsersRepository
    {
        public List<User> Users { get; set; }
        public User User { get; set; }
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        /*public User Get(int UserId)
        {
            var sql = "SELECT * FROM gebruiker";
            using var connection = GetConnection();
            var user = connection.QuerySingle<User>(sql, new { UserId });
            //GetIe();

            return user;
        }*/
        public IEnumerable<User> GetIe()
        {
            var sql = "SELECT * FROM gebruiker";

            using var connection = GetConnection();
            var naam = connection.Query<User>(sql);
            foreach (var user in naam)
            
            {
                AddToList(user);
            }
            return naam;
           // Users.AddRange(naam.ToList());
        
        }
        public void AddToList(User u)
        {
            User.Users.Add(u);
        }
        /*public bool Delete(int snackId)
        {
            string sql = @"DELETE FROM snack WHERE id = @snackId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { snackId });
            return numOfEffectedRows == 1;
        }*/

    }
}