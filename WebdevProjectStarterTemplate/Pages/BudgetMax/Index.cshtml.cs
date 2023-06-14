using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.BudgetMax
{
    public class IndexModel : PageModel
    {
        public Budget budgets { get; set; } = null!;

        public void OnGet()
        {
            budgets = new BudgetRepository().Get();
        }
    }
}
