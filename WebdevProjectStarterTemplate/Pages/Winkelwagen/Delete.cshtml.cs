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
      
        public Bestelling Bestelling { get; set; } = null!;

        public void OnGet([FromRoute] int bestellingId)
        {
            Bestelling = new BestellingRepository().Get(bestellingId);
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
