using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages.History;

public enum TijdsRelativiteit
{
    Tijdens,
    Voor,
    Na
}

public class Index : PageModel
{
    public IEnumerable<Models.History> History;
    public IEnumerable<int> BeschikbareJaren;
    public IEnumerable<int>? BeschikbareWeken;
    
    public int? geselecteerdJaar;
    public int? geselecteerdeWeek;
    public TijdsRelativiteit? GeselecteerdeRelativiteit;
    

    public void OnGet(int? jaar, int? week, TijdsRelativiteit? relativiteit)
    {
        BeschikbareJaren = new HistoryRepository().GetAvailableYears();
        if (jaar is not null)
        {
            geselecteerdJaar = jaar;
            BeschikbareWeken = new HistoryRepository().GetAvailableWeeks((int)jaar);
        }

        if (week is not null)
        {
            geselecteerdeWeek = week;
        }

        if (relativiteit is not null)
        {
            GeselecteerdeRelativiteit = relativiteit;
        }
        History = new HistoryRepository().Get();
        Console.WriteLine(relativiteit);
    }
}