using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Update : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;
        AccountController accountController = new AccountController();
        
        public IEnumerable<Snack>? Snacks;
        
        // Handler voor GET-verzoek bij het ophalen van de Snackbar voor bewerking
        public void OnGet(int snackbarId)
        {
            Snackbar = new SnackbarRepository().Get(snackbarId);
            Snacks = new SnackRepository().GetFromSnackBar(snackbarId);
        }
        
        // "Bijwerken" knop
        public IActionResult OnPost(Snackbar snackbar)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new SnackbarRepository().Update(snackbar);
            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina na succesvol bijwerken
        }
        
        // "Anuleren" knop
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina bij annuleren
        }
    }
}

