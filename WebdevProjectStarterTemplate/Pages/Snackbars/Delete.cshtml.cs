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
        // "Verwijder" knop
        public IActionResult OnPostDelete([FromRoute] int snackbarId)
        {
            bool success = new SnackbarRepository().Delete(snackbarId); // TODO: handle success = false
            return RedirectToPage(nameof(Index));
        }
        // "Anuleer" knop
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}

