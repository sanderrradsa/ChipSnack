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
            // Haal de categorie op met het opgegeven CategorieId
            Categorie = new CategorieRepository().Get(CategorieId);
        }
        public IActionResult OnPostDelete([FromRoute] int CategorieId)
        {
            // Verwijder de categorie met het opgegeven CategorieId
            bool success = new CategorieRepository().Delete(CategorieId);

            // Doorverwijzen naar de indexpagina van categorieën
            return RedirectToPage(nameof(Index));
        }
        public IActionResult OnPostCancel()
        {
            // Annuleren en doorverwijzen naar de indexpagina van categorieën
            return RedirectToPage(nameof(Index));
        }
    }
}
