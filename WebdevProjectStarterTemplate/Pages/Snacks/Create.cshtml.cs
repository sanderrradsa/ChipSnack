using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class Create : PageModel
    {
        [BindProperty] public Snack snack { get; set; } = null!;
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategorie = new List<Categorie>();
        public void OnGet()
        {
            GetSnackbars();
            GetCategorie();
        }
        public void GetSnackbars()
        {
            var Snackbars = new SnackbarRepository().Get();
            foreach (var snackbar in Snackbars)
            {
                ListSnackBars.Add(snackbar);
            }
        }
        public void GetCategorie()
        {
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

            var createdSnack = new SnackReposiroty().Add(snack);

            return RedirectToPage("Index");
        }


        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Index");
        }
    }
}
