using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;

[Authorize]
public class Index : PageModel
{
    public IEnumerable<Categorie> Categories { get; set; } = null!;
    public IEnumerable<Categorie> CategoriesWithProduct { get; set; } = null!;
    
    public void OnGet()
    {
        Categories = new CategorieRepository().Get();
    }

    
}