using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Delete : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;
        // Handler voor GET-verzoek bij het ophalen van de Snackbar voor verwijdering
        public void OnGet([FromRoute] int snackbarId)
        {
            Snackbar = new SnackbarRepository().Get(snackbarId);
        }
        // Handler voor POST-verzoek bij het verwijderen van de Snackbar
        public IActionResult OnPostDelete([FromRoute] int snackbarId)
        {
            bool success = new SnackbarRepository().Delete(snackbarId);
            return RedirectToPage(nameof(Index));
        }
        // Handler voor POST-verzoek bij annuleren
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}

