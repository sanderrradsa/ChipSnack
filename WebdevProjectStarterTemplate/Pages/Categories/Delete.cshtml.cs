using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;

public class Delete : PageModel
{
    public Categorie Categorie { get; set; } = null!;
    
    public void OnGet([FromRoute] int CategorieId)
    {
        Categorie = new CategorieRepository().Get(CategorieId);
    }

    public IActionResult OnPostDelete([FromRoute]int CategorieId)
    {
        bool success = new CategorieRepository().Delete(CategorieId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}