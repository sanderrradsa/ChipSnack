using Dapper;
using static Dapper.SqlMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
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

        public IEnumerable<Snack> GetFromCategory(int categoryId)
        {
            string sql = @"SELECT * FROM snack WHERE categorieId = @categoryId";

            using var connection = GetConnection();
            var snacks = connection.Query<Snack>(sql, new { categoryId });
            return snacks;
        }

        public IEnumerable<Snack> GetFromBarAndCategory(int snackbarId, int categoryId)
        {
            string sql = @"SELECT * FROM snack WHERE snackbarId = @snackbarId AND categorieId = @categoryID";

            using var connection = GetConnection();
            var snacks = connection.Query<Snack>(sql, new { snackbarId, categoryId });
            return snacks;
        }


        public bool Delete(int snackId)
        {
            string sql = @"DELETE FROM snack WHERE id = @snackId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { snackId });
            return numOfEffectedRows == 1;
        }
        public Snack Add(Snack snack)
        {
            Console.WriteLine(snack.Naam);
            const string sql = @"
        INSERT INTO snack (naam, prijs, beschrijving, snackbarid, categorieId)
        VALUES (@Naam, @Prijs, @Beschrijving, @Snackbarid, @CategorieId);
        SELECT * FROM snack WHERE id = LAST_INSERT_ID()";
            using var connection = GetConnection();
            var addedSnack = connection.QuerySingle<Snack>(sql, snack);
            return addedSnack;
        }
        //public Snack Add(Snack snack, string Naam, int Prijs, string Beschrijving)
        //    {
        //        const string sql = @"
        //        INSERT INTO snackbar (Naam, Prijs, Beschrijving)
        //        VALUES (@Naam, @Prijs, @Beschrijving);
        //        SELECT * FROM snackbar WHERE id = LAST_INSERT_ID()";

        //        using var connection = GetConnection();
        //        using var command = new MySqlCommand(sql, connection);
        //        command.Parameters.AddWithValue("@Naam", Naam);
        //        command.Parameters.AddWithValue("@Prijs", Prijs);
        //        command.Parameters.AddWithValue("@Beschrijving", Beschrijving);

        //        connection.Open();
        //        var addedSnack = command.ExecuteScalar();

        //        snack.Naam = Naam;
        //        snack.Prijs = Prijs;
        //        snack.Beschrijving = Beschrijving;
        //        snack.Id = Convert.ToInt32(addedSnack);

        //        return snack;
        //    }





        public Snack Update(Snack snack)
        {
            string sql = @"
        UPDATE snack SET 
            naam = @Naam,
            prijs = @Prijs,
            beschrijving = @Beschrijving,
            snackbarId = @Snackbarid,
            categorieID = @CategorieID
        WHERE id = @Id;
        SELECT * FROM snack WHERE id = @Id";

            using var connection = GetConnection();
            var updatedSnack = connection.QuerySingle<Snack>(sql, snack);
            //var updatedSnack = connection.Query<Snack>(sql, snack).SingleOrDefault();
            return updatedSnack;
        }

    }
}
