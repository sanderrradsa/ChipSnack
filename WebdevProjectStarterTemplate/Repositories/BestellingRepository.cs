using System.Data;
using Dapper;
using System.Collections.Generic;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class BestellingRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public IEnumerable<Bestelling> GetBestellingWithSnack(int gebruikerId)
        {
            string sql = @"SELECT * FROM bestelling as b JOIN snack as s ON b.snackId = s.id WHERE gebruikerId = @gebruikerId";

            using var connection = GetConnection();
            var BestellingWithSnack = connection.Query<Bestelling, Snack, Bestelling>(
            sql,
                map: (bestelling, snack) =>
                {
                    bestelling.Snack = snack;
                    return bestelling;
                },
                            new { gebruikerId },
                splitOn: "snackId"
            );
            return BestellingWithSnack;
        }
        public Bestelling Update(int aantall, int bestellingId)
        {

            string sql = @"
                UPDATE bestelling SET 
                    aantal = aantal + @aantall
                WHERE id = @bestellingId;
                SELECT * FROM bestelling WHERE id = @bestellingId";

            using var connection = GetConnection();
            var updatedOrder = connection.QuerySingle<Bestelling>(sql, new { Aantal = aantall, Id = bestellingId });
            return updatedOrder;
        }

    }
}
