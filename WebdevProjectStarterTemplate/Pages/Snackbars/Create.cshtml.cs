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
        /// <summary>Gebind aan form in de cshtml, inputs worden automatisch gebruikt voor het invullen van de moddel</summary>
        [BindProperty] public Snackbar Snackbar { get; set; } = null!;
        // Handler voor GET-verzoek
        public void OnGet()
        {
            //
        }
        
        // "creeer knop"
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Als de modelvalidatie mislukt, blijf op dezelfde pagina en geef foutmeldingen weer
            }
            // Voeg de nieuwe Snackbar toe aan de db
            new SnackbarRepository().Add(Snackbar);
            return RedirectToPage(nameof(Index));
        }
        // "anuleren" knop
        public IActionResult OnPostCancel()
        {
            return Redirect(nameof(Index));
        }
    }
}

