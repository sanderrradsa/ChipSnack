﻿using System.Data;
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
        public IEnumerable<Bestelling> Get()
        {
            string sql = "SELECT * FROM bestelling";

            using var connection = GetConnection();
            var bestellingen = connection.Query<Bestelling>(sql);
            return bestellingen;
        }
        public Bestelling Get(int bestellingId)
        {
            string sql = "SELECT * FROM bestelling WHERE id = @bestellingId";

            using var connection = GetConnection();
            var bestelling = connection.QuerySingle<Bestelling>(sql, new { bestellingId });
            return bestelling;
        }
        public Bestelling Add(int week, int year, int userid, int snackId, string opmerking)
        {
            string sql = "INSERT INTO bestelling (gebruikerId, aantal, opmerking, weeknr, jaar, snackId) VALUES (@userid,1, @opmerking,@week,@year,@snackId); SELECT * FROM bestelling WHERE id = LAST_INSERT_ID()";

            using var connection = GetConnection();
            var bestelling = connection.QuerySingle<Bestelling>(sql, new { week, year, userid, snackId, opmerking});
            return bestelling;
        }
        public bool Delete(int bestellingId)
        {
            string sql = @"DELETE FROM bestelling WHERE id = @bestellingId";

            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { bestellingId });
            return numOfEffectedRows == 1;
        }
        public IEnumerable<Bestelling> GetBestellingWithSnack(int year, int week, int userId)
        {
            string sql = @"SELECT * FROM bestelling as b JOIN snack as s ON b.snackId = s.id WHERE jaar = @year AND weeknr = @week AND gebruikerId = @userId";

            using var connection = GetConnection();
            var BestellingWithSnack = connection.Query<Bestelling, Snack, Bestelling>(
            sql,
                map: (bestelling, snack) =>
                {
                    bestelling.Snack = snack;
                    return bestelling;
                },
                            new { year, week, userId},
                splitOn: "Id"
            );
            return BestellingWithSnack;
        }
        public Bestelling Update(int aantal, int bestellingId)
        {

            string sql = @"
                UPDATE bestelling SET 
                    aantal = aantal + @aantal
                WHERE id = @bestellingId;
                SELECT * FROM bestelling WHERE id = @bestellingId";

            using var connection = GetConnection();
            var updatedOrder = connection.QuerySingle<Bestelling>(sql, new { aantal, bestellingId });
            return updatedOrder;
        }
        
        public IEnumerable<Bestelling> UpdateLockIn(int year, int week, int gebruikerId)
        {

            string sql = @"
                UPDATE bestelling SET 
                    bevestigd = 1
                WHERE jaar = @year AND
                weeknr = @week AND
                gebruikerId = @gebruikerId;
                SELECT * FROM bestelling WHERE jaar = @year AND weeknr = @week AND gebruikerId = @gebruikerId";

            using var connection = GetConnection();
            var updatedOrder = connection.Query<Bestelling>(sql, new { year, week, gebruikerId });
            return updatedOrder;
        }
    }
}
