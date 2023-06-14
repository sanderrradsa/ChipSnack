using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Update : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;
        AccountController accountController = new AccountController();
        public void OnGet(int snackbarId)
        {

            Snackbar = new SnackbarRepository().Get(snackbarId);
            //snack = new SnackReposiroty().Get(snackId);
            //GetSnackbars();
            GetSnacks(snackbarId);
            GetCategorie();
        }

        public IActionResult OnPost(Snackbar snackbar)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedSnackbar = new SnackbarRepository().Update(snackbar);

            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
        [BindProperty]
        public Snack snack { get; set; } = null!;
        public List<Snack> ListSnacks = new List<Snack>();
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategorie = new List<Categorie>();
        
        public void getSnackUpdate(int snackId)
        {
            snack = new SnackReposiroty().Get(snackId);
            //GetSnackbars();
            GetCategorie();
        }

        public void GetSnacks(int snackbarId)
        {
            var Snack = new SnackReposiroty().Get();   
            foreach(var snack in Snack){
                if(snack.Snackbarid == snackbarId)
                {
                    ListSnacks.Add(snack);
                }
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
        public IActionResult OnPost(Snack snack)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedSnack = new SnackReposiroty().Update(snack);

            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancl1()
        {
            return RedirectToPage(nameof(Index));
        }
    
}
}
