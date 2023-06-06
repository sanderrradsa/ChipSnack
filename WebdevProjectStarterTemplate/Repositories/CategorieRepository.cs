using System.Collections;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    [Authorize]
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
            string sql = "SELECT * FROM categorie ORDER BY naam";

            using var connection = GetConnection();
            var categories = connection.Query<Categorie>(sql);
            return categories;
        }

        public Categorie Add(Categorie? Categorie)
        {
            string sql = @"
                INSERT INTO categorie (naam) 
                VALUES (@Name); 
                SELECT * FROM categorie WHERE id = LAST_INSERT_ID()";
            
            using var connection = GetConnection();
            var addedCategorie = connection.QuerySingle<Categorie>(sql, Categorie);
            return addedCategorie;
        }

        public bool Delete(int CategorieId)
        {
            string sql = @"DELETE FROM categorie WHERE id = @categoryId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { CategorieId });
            return numOfEffectedRows == 1;
        }

        public Categorie Update(Categorie Categorie)
        {
            string sql = @"
                UPDATE categorie SET 
                    naam = @Name 
                WHERE id = @CategoryId;
                SELECT * FROM categorie WHERE id = @CategoryId";
            
            using var connection = GetConnection();
            var updatedCategorie = connection.QuerySingle<Categorie>(sql, Categorie);
            return updatedCategorie;
        }
    }
}