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
        [BindProperty] public Categorie Category { get; set; } = null!;

        public void OnGet()
        {
        }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createdCategorie = new CategorieRepository().Add(Categorie);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
    }
}