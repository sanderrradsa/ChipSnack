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
            Categorie = new CategorieRepository().Get(CategorieId);
            GetSnacks(CategorieId);
        }

        public IActionResult OnPost(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedCategorie = new CategorieRepository().Update(categorie);

            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }

        public void GetSnacks(int categorieId)
        {
            var snacks = new SnackReposiroty().Get();
            foreach (var snack in snacks)
            {
                if (snack.CategorieId == categorieId)
                {
                    ListSnacks.Add(snack);
                }
            }
        }
    }
}
