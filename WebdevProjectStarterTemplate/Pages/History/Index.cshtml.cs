using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Repositories;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Pages.History;

public class Index : PageModel
{
    public IEnumerable<Models.History> History;
    public void OnGet()
    {
        History = new HistoryRepository().Get();
    }
}