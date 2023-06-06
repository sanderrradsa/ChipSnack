using System.Collections;
using System.Data;
using Dapper;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
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
                INSERT INTO Categorie (Name) 
                VALUES (@Name); 
                SELECT * FROM Categorie WHERE CategorieId = LAST_INSERT_ID()";
            
            using var connection = GetConnection();
            var addedCategorie = connection.QuerySingle<Categorie>(sql, Categorie);
            return addedCategorie;
        }

        public bool Delete(int CategorieId)
        {
            string sql = @"DELETE FROM Categorie WHERE CategorieId = @CategorieId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { CategorieId });
            return numOfEffectedRows == 1;
        }

        public Categorie Update(Categorie Categorie)
        {
            string sql = @"
                UPDATE Categorie SET 
                    Name = @Name 
                WHERE CategorieId = @CategorieId;
                SELECT * FROM Categorie WHERE CategorieId = @CategorieId";
            
            using var connection = GetConnection();
            var updatedCategorie = connection.QuerySingle<Categorie>(sql, Categorie);
            return updatedCategorie;
        }
    }
}