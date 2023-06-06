using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;
[Authorize]
public class Update : PageModel
{
    public Categorie Categorie { get; set; } = null!;
    
    public void OnGet(int CategorieId)
    {
        Categorie = new CategorieRepository().Get(CategorieId);
    }

    public IActionResult OnPost(Categorie Categorie)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var updatedCategorie = new CategorieRepository().Update(Categorie);

        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}