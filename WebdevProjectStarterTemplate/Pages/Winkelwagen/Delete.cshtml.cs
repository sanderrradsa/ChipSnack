using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Winkelwagen
{
    [Authorize]
    public class Delete : PageModel
    {
        public Snack snack { get; set; }
        public Bestelling Bestelling { get; set; } = null!;
        /// <summary>
        /// Haalt de snack op voor de gegeven bestellingId en stelt deze in als snack-eigenschap.
        /// </summary>
        /// <param name="bestellingId">Id van de bestelling</param>
        public void OnGet([FromRoute] int bestellingId)
        {
            Bestelling getsnackId = new BestellingRepository().Get(bestellingId);
            snack = new SnackReposiroty().Get(getsnackId.SnackId);
        }
        /// <summary>
        /// Verwijdert de bestelling met de gegeven bestellingId.
        /// </summary>
        /// <param name="bestellingId">Id van de bestelling</param>
        /// <returns>Redirect naar de Index-pagina</returns>
        public IActionResult OnPostDelete([FromRoute] int bestellingId)
        {
            bool success = new BestellingRepository().Delete(bestellingId);
            return RedirectToPage(nameof(Index));
        }

        /// <summary>
        /// Annuleert de huidige bewerking en keert terug naar de Index-pagina.
        /// </summary>
        /// <returns>Redirect naar de Index-pagina</returns>
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}

