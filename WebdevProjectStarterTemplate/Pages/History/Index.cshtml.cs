using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;
// using 

namespace WebdevProjectStarterTemplate.Pages.History;

/// Beschrijft de relatie tot een punt in tijd (te gebruiken voor filter etc)
public enum TijdsRelativiteit
{
    Tijdens,
    TotEnMet,
    Vanaf,
    /// Alles van één moment tot en met een ander moment
    VanTotEnMet
}
/// Utilities voor: <see cref="TijdsRelativiteit"/>
public class TijdsRelativiteitUtil
{
    public static string DisplayText(TijdsRelativiteit relativiteit)
    {
        return relativiteit switch
        {
            TijdsRelativiteit.TotEnMet => "Tot en met",
            TijdsRelativiteit.VanTotEnMet => "Van, Tot en met",
            _ => relativiteit.ToString() // zelfde als de naam in de enum
        };
    }
}

public class Index : PageModel
{
    public IEnumerable<Models.History> History;
    public IEnumerable<int> BeschikbareJaren;

    public int? geselecteerdJaar;
    public int? geselecteerdeWeek;

    public int? geselecteerdEindJaar;
    public int? geselecteerdeEindWeek;
    public TijdsRelativiteit GeselecteerdeRelativiteit;


    public void OnGet(int? jaar, int? week, int? eindJaar, int? eindWeek, TijdsRelativiteit relativiteit = TijdsRelativiteit.Tijdens)
    {
        var huidigeDatum = DateTime.Today;
        var cal = new CultureInfo("en-US").Calendar;

        // Variabele die de huidige week van het jaar bevat, op basis van de gegeven kalenderinstellingen
        var huidigeWeek = cal.GetWeekOfYear(huidigeDatum, CalendarWeekRule.FirstDay, DayOfWeek.Monday) - 1;

        // Variabele die het huidige jaar bevat, uit de huidige datum
        var huidigJaar = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
        geselecteerdJaar = jaar ?? huidigJaar;

        // De geselecteerde week en of jaar die wordt toegewezen aan het model
        geselecteerdeWeek = week ?? huidigeWeek;
        geselecteerdEindJaar = eindJaar;
        geselecteerdeEindWeek = eindWeek;
        BeschikbareJaren = new HistoryRepository().GetAvailableYears();

        // De geselecteerde tijdsrelativiteit die wordt toegewezen aan het model, standaard ingesteld op "Tijdens" indien niet opgegeven
        GeselecteerdeRelativiteit = relativiteit;

        if (geselecteerdJaar is not null)
        {
            // Haal de bijbehorende geschiedenis op basis van de opgegeven parameters
            History = new HistoryRepository().Get(GeselecteerdeRelativiteit, (int)geselecteerdJaar, geselecteerdeWeek, geselecteerdEindJaar, geselecteerdeEindWeek);
        }
        else
        {
            // Haal alle geschiedenis op via de HistoryRepository
            History = new HistoryRepository().Get();
        }
    }

}