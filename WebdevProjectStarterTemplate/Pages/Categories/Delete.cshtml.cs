using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories
{
    public class Delete : PageModel
    {
        public Categorie Categorie { get; set; } = null!;
        public void OnGet([FromRoute] int CategorieId)
        {
            Categorie = new CategorieRepository().Get(CategorieId);
        }
        
        // Verwijder knop
        public IActionResult OnPostDelete([FromRoute] int CategorieId)
        {
            // TODO: succes = false afhandelen
            bool success = new CategorieRepository().Delete(CategorieId);

            // Categorie overzicht
            return RedirectToPage(nameof(Index));
        }
        
        // Anuleer knop
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}
