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

        public void OnGet([FromRoute] int snackId)
        {
            snack = new SnackReposiroty().Get(snackId);

        }
        public IActionResult OnPostDelete([FromRoute] int bestellingId)
        {
            bool success = new BestellingRepository().Delete(bestellingId);
            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    
    }
}
