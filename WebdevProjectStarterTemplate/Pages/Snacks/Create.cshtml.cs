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
        AccountController _accountController = new AccountController();
        public void OnGet()
        {
            snack = new Snack();
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
 
            // if (!ModelState.IsValid)
            // {
            //     return Redirect("~/Snackbars/Index");
            // }
            // TODO: ModelState.Isvalid returned altijd false??? andere manier om te validaten, of meer onderzoek naar 'IsValid'


            var createdSnack = new SnackReposiroty().Add(snack);

            return Redirect("~/Snackbars/Index");
        }



    }
}
