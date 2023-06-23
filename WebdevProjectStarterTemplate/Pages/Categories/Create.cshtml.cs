using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories
{
    [Authorize]
    public class Create : PageModel
    {
        [BindProperty]
        public Categorie categorie { get; set; } = null!;
        
        
        // Voeg de nieuwe categorie toe (Aanmaken knop)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            new CategorieRepository().Add(categorie);
            // Terug naar overzicht
            return RedirectToPage("Index");
        }
        
        // "Anuleer knop"
        public IActionResult OnPostCancel()
        {
            return Redirect(nameof(Index));
        }
    }
}
