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

		var dt = DateTime.Today;
        Calendar cal = new CultureInfo("en-US").Calendar;
        int currentWeek = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) -1;
        string date = DateTime.Now.ToString("yyyy");
        int currentYear = Convert.ToInt32(date);		

        geselecteerdJaar = jaar ?? currentYear; //ziet er dom uit, maar deze worden nu toegankelijk aan de model.
        geselecteerdeWeek = week ?? currentWeek;

        geselecteerdEindJaar = eindJaar;
        geselecteerdeEindWeek = eindWeek;

        BeschikbareJaren = new HistoryRepository().GetAvailableYears();

        GeselecteerdeRelativiteit = relativiteit;
        
        if (geselecteerdJaar is not null)
        {
            History = new HistoryRepository().Get(GeselecteerdeRelativiteit, (int)geselecteerdJaar, geselecteerdeWeek,
                geselecteerdEindJaar, geselecteerdeEindWeek);
        }
        else
        {
            History = new HistoryRepository().Get();
        }
    }
}