using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty] public Snackbar Snackbar { get; set; } = null!;
        // Handler voor GET-verzoek
        public void OnGet()
        {
        
        }
        // Handler voor POST-verzoek bij het maken van een nieuwe Snackbar
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Als de modelvalidatie mislukt, blijf op dezelfde pagina en geef foutmeldingen weer
            }
            // Voeg de nieuwe Snackbar toe aan de repository
            var createdSnackbar = new SnackbarRepository().Add(Snackbar);
            return RedirectToPage(nameof(Index));
        }
        // Handler voor POST-verzoek bij annuleren
        public IActionResult OnPostCancel()
        {
            return Redirect(nameof(Index));
        }
    }
}

