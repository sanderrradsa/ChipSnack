using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class Update : PageModel
    {
        [BindProperty]
        public Snack snack { get; set; } = null!;
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategorie = new List<Categorie>();
        //public SelectList Categorieen { get; set; }
        //public async Task<IActionResult> OnGetAsync(int snackId)
        //{
        //    Snack = await SnackRepository.GetByIdAsync(snackId);

        //    if (Snack == null)
        //    {
        //        return NotFound();
        //    }

        //    Categorieen = new SelectList(await CategorieRepository.GetAllAsync(), "Id", "Naam", Snack.CategorieID);

        //    return Page();
        //}

        public void OnGet(int snackId)
        {
            snack = new SnackReposiroty().Get(snackId);
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
        public IActionResult OnPost(Snack snack)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("~/Snackbars/Index");
            }

            var updatedSnack = new SnackReposiroty().Update(snack);

            return Redirect("~/Snackbars/Index");
        }

        /*public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }*/
    }
}
