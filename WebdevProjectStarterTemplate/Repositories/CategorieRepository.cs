using System.Collections;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
   // [Authorize]
    public class CategorieRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Categorie Get(int Id)
        {
            string sql = "SELECT * FROM Categorie WHERE id = @Id";

            using var connection = GetConnection();
            var Categorie = connection.QuerySingle<Categorie>(sql, new { Id });
            return Categorie;
        }

        public IEnumerable<Categorie> Get()
        {
            string sql = "SELECT * FROM categorie"; //ORDER BY naam

            using var connection = GetConnection();
            var categories = connection.Query<Categorie>(sql);
            return categories;
        }

        public Categorie Add(Categorie categorie)
        {
            const string sql = @"
                INSERT INTO categorie (naam) 
                VALUES (@Naam); 
                SELECT * FROM categorie WHERE id = LAST_INSERT_ID()";

            using var connection = GetConnection();
            var addedCategorie = connection.QuerySingle<Categorie>(sql, categorie);
            return addedCategorie;
        }

        public bool Delete(int Id)
        {
            string sql = @"DELETE FROM categorie WHERE id = @Id";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { Id });
            return numOfEffectedRows == 1;
        }

        public Categorie Update(Categorie categorie)
        {
            string sql = @"
                UPDATE categorie SET 
                    naam = @Naam 
                WHERE id = @Id;
                SELECT * FROM categorie WHERE id = @Id";
            
            using var connection = GetConnection();
            var updatedCategorie = connection.QuerySingle<Categorie>(sql, categorie);
            return updatedCategorie;
        }
    }
}