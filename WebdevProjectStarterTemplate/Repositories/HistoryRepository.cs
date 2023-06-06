using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories;

public class HistoryRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    public IEnumerable<History> Get()
    {
        const string sql = "SELECT SUM(b.aantal) AS totaal, b.jaar, b.weeknr, b.snackId, s.naam, s.snackbarId, s.categorieId, s.beschrijving FROM bestelling AS b " +
                           "JOIN snack s on b.snackId = s.id " +
                           "WHERE b.herhalen = 0 " +
                           "GROUP BY b.snackId;";
        
        using var connection = GetConnection();

        var history = connection.Query<History, Snack, History>(sql, 
                map: (history, snack) =>
                {
                    history.Snack = snack;
                    return history;
                },
                
                splitOn: "snackId"
            );
        return history;
    }

    public IEnumerable<int> GetAvalibleWeeks(int jaar)
    {
        const string sql = "SELECT DISTINCT weeknr FROM bestelling " +
                           "WHERE jaar = @jaar ;";

        using var connection = GetConnection();
        var weeks = connection.Query<int>(sql, new { jaar });

        return weeks;
    }
} 