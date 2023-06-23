using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.BudgetMax
{
    public class Update : PageModel
    {
        public Budget budget { get; set; } = null!;
        public void OnGet(int budgetId)
        {
            budget = new BudgetRepository().Get(budgetId);
        }
        
        // het bijwerken van het budget
        public IActionResult OnPost(Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var updatedBudget = new BudgetRepository().Update(budget);
            return RedirectToPage(nameof(Index));
        }
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}

