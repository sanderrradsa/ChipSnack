using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories
{
    [Authorize]
    public class Update : PageModel
    {
        public Categorie Categorie { get; set; } = null!;
        public IEnumerable<Snack> Snacks;
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategories = new List<Categorie>();
        public void OnGet(int CategorieId)
        {
            // Haal de categorie uit db, voor huidige info
            Categorie = new CategorieRepository().Get(CategorieId);
            // Snackoverzicht bij categorie
            Snacks = new SnackRepository().GetFromCategory(CategorieId);
        }
        public IActionResult OnPost(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Update de categorie met de gewijzigde gegevens in db
            new CategorieRepository().Update(categorie);

            return RedirectToPage(nameof(Index));
        }
        public IActionResult OnPostCancel()
        {
            // Redirect naar de Index-pagina
            return RedirectToPage(nameof(Index));
        }
    }
}

