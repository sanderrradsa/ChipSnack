using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages.History;

public class Index : PageModel
{
    public IEnumerable<Models.History> History;
    public IEnumerable<int> BeschikbareJaren;
    public IEnumerable<int>? BeschikbareWeken;

    public int? geselecteerdJaar;
    public int? geselecteerdeWeek;
    
    public enum TijdsRelativiteit
    {
        Tijdens,
        Voor,
        Na
    }
    public void OnGet(int? jaar, int? week, TijdsRelativiteit? relativiteit)
    {
        BeschikbareJaren = new HistoryRepository().GetAvailableYears();
        if (jaar is not null)
        {
            geselecteerdJaar = jaar;
            BeschikbareWeken = new HistoryRepository().GetAvailableWeeks((int)jaar);
        }
        History = new HistoryRepository().Get();
        Console.WriteLine(relativiteit);
    }
}