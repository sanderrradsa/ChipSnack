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

        public List<Categorie> ListCategorie = new List<Categorie>();
        public void OnGet()
        {
            // Haal alle categorieën op
            GetCategorie();
        }
        public void GetCategorie()
        {
            // Haal alle categorieën op en voeg ze toe aan de lijst
            var Categorie = new CategorieRepository().Get();
            foreach (var categorie in Categorie)
            {
                ListCategorie.Add(categorie);
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Voeg de nieuwe categorie toe
            var createdCategorie = new CategorieRepository().Add(categorie);
            // Doorverwijzen naar de indexpagina van categorieën
            return RedirectToPage("Index");
        }
        public IActionResult OnPostCancel()
        {
            // Annuleren en doorverwijzen naar de indexpagina van categorieën
            return Redirect(nameof(Index));
        }
    }
}
