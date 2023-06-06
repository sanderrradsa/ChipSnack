using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    public class Update : PageModel
    {
        [BindProperty]
        public Snack Snack { get; set; } = null!;
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
             Snack = new SnackReposiroty().Get(snackId);
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

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }



}
