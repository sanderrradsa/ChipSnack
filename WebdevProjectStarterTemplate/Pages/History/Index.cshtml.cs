using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages.History;


public enum TijdsRelativiteit
{
    Tijdens,
    TotEnMet,
    Vanaf,
    VanTotEnMet
}

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

        geselecteerdJaar = jaar; //ziet er dom uit, maar deze worden nu toegangbaar aan de model.
        geselecteerdeWeek = week;

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