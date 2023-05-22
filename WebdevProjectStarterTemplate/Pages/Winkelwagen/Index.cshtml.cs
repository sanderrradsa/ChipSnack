using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Winkelwagen
{
    public class Index : PageModel
    {
        public IEnumerable<Bestelling> Bestelling { get; set; } = null!;
        public void OnGet()
        {
            Bestelling = new BestellingRepository().GetBestellingWithSnack(1);
        }
        public IActionResult OnPostIncrement(int bestellingId)
        {
                var updatedOrder = new BestellingRepository().Update(1, bestellingId);
            OnGet();

            return Page();

        }
    }
}
