using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.BudgetMax
{
    public class Update : PageModel
    {
        public Budget budget { get; set; } = null!;
        // Handler voor GET-verzoek
        public void OnGet(int budgetId)
        {
            // Haal het budget op met het gegeven budgetId uit de repository
            budget = new BudgetRepository().Get(budgetId);
        }
        // Handler voor POST-verzoek bij het bijwerken van het budget
        public IActionResult OnPost(Budget budget)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Als de modelvalidatie mislukt, blijf op dezelfde pagina en geef foutmeldingen weer
            }
            // Werk het budget bij in de repository
            var updatedBudget = new BudgetRepository().Update(budget);
            return RedirectToPage(nameof(Index));
        }
        // Handler voor POST-verzoek bij annuleren
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}

