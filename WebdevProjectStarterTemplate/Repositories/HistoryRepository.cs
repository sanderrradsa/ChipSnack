using System.Collections;
using Dapper;
using System.Data;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Pages;
using WebdevProjectStarterTemplate.Pages.History;

namespace WebdevProjectStarterTemplate.Repositories;

public class HistoryRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    public IEnumerable<History> Get()
    {
        const string sql = "SET sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY','')); " + // Verwijdert de db manager regel dat een group by allen bepaalde acties kan uitvoeren op de colommen gebruikt in de GROUP BY selectie.
                           "SELECT SUM(b.aantal) AS totaal, b.jaar, b.weeknr, b.snackId, s.naam, s.snackbarId, s.prijs, s.categorieId, s.beschrijving FROM bestelling AS b " +
                           
                           "JOIN snack s on b.snackId = s.id " +
                           "WHERE b.bevestigd = 1 " +
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

    public IEnumerable<History> Get(TijdsRelativiteit relativiteit,
        int startJaar, int? startWeek,
        int? eindJaar, int? eindWeek)
    {

        StringWriter sql = new StringWriter();// Deze functie is dynamiser, daarom afgeweken van de standaard string
        sql.WriteLine("SET sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY','')); ");
        sql.WriteLine(
            "SELECT SUM(b.aantal) AS totaal, b.jaar, b.weeknr, b.snackId, s.naam, s.snackbarId, s.prijs, s.categorieId, s.beschrijving " +
            "FROM bestelling AS b " +
            "JOIN snack s on b.snackId = s.id ");
    
        switch (relativiteit)
        {
            case TijdsRelativiteit.Tijdens:
                sql.WriteLine(@"WHERE b.jaar = @startJaar ");
                if (startWeek is not null)
                {
                    sql.WriteLine(@"AND b.weeknr = @startWeek ");
                }
                break;
            
            case TijdsRelativiteit.Vanaf:
                sql.WriteLine(@"WHERE ( b.jaar >= @startJaar ");
                if (startWeek is not null)
                {
                    sql.WriteLine(@"AND b.weeknr >= @startWeek ) OR b.jaar > @startJaar ");
                }
                else
                {
                    sql.WriteLine(") ");
                }
                break;
            
            case TijdsRelativiteit.TotEnMet:
                sql.WriteLine(@"WHERE ( b.jaar <= @startJaar ");
                if (startWeek is not null)
                {
                    sql.WriteLine("AND b.weeknr <= @startWeek ) OR b.jaar < @startJaar ");
                }
                else
                {
                    sql.WriteLine(") ");
                }
                break;
            
            case TijdsRelativiteit.VanTotEnMet: // help :(
                if (eindJaar is null)
                {
                    throw new NoNullAllowedException(
                        "param 'eindjaar' kan niet null zijn wanneer param 'relativiteit' is 'VanTotEnMet'");
                }
                
                sql.WriteLine(@"WHERE b.jaar BETWEEN @startJaar AND @eindJaar ");

                if (startWeek is not null)
                {
                    // TODO: dit genereerd niet bepaald efficiente queries, en deze kan wel op veel rows worden uitgevoerd
                    sql.WriteLine("AND ( b.weeknr > @startWeek OR b.jaar > @startJaar ) ");
                }

                if (eindWeek is not null)
                {
                    sql.WriteLine("AND ( b.weeknr < @eindWeek OR b.jaar < @eindJaar ) ");
                }
                break;
            
            default:
                throw new NotImplementedException(
                    $"TijdsRelativiteit `{relativiteit.ToString()}` heeft geen SQL query builder");
        }
        sql.WriteLine("GROUP BY b.snackId;");
        Console.WriteLine(sql.ToString());

        using var connection = GetConnection();
        var history = connection.Query<History, Snack, History>(sql.ToString(), 
            map: (history, snack) =>
            {
                history.Snack = snack;
                return history;
            },
                
            splitOn: "snackId",
            param: new {startJaar, startWeek, eindJaar, eindWeek}
        );
        return history;
    }

    // public IEnumerable<int> GetAvailableWeeks(int jaar)
    // {
    //     const string sql = "SELECT DISTINCT weeknr FROM bestelling " +
    //                        "WHERE jaar = @jaar ;";
    //
    //     using var connection = GetConnection();
    //     var weeks = connection.Query<int>(sql, new { jaar });
    //
    //     return weeks;
    // }

    public IEnumerable<int> GetAvailableYears()
    {
        const string sql = "SELECT DISTINCT jaar FROM bestelling";

        using var connection = GetConnection();
        var years = connection.Query<int>(sql);

        return years;
    }
} 