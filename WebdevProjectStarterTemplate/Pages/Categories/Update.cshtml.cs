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
        public List<Snack> ListSnacks = new List<Snack>();
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategories = new List<Categorie>();
        public void OnGet(int CategorieId)
        {
            // Haal de categorie op met de opgegeven CategorieId
            Categorie = new CategorieRepository().Get(CategorieId);
            // Haal snacks op die bij de categorie horen
            GetSnacks(CategorieId);
        }
        public IActionResult OnPost(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Update de categorie met de gewijzigde gegevens
            var updatedCategorie = new CategorieRepository().Update(categorie);

            return RedirectToPage(nameof(Index));
        }
        public IActionResult OnPostCancel()
        {
            // Redirect naar de Index-pagina
            return RedirectToPage(nameof(Index));
        }
        public void GetSnacks(int categorieId)
        {
            // Haal alle snacks op
            var snacks = new SnackReposiroty().Get();
            foreach (var snack in snacks)
            {
                // Voeg snacks toe aan de lijst die overeenkomen met de opgegeven categorieId
                if (snack.CategorieId == categorieId)
                {
                    ListSnacks.Add(snack);
                }
            }
        }
    }
}

