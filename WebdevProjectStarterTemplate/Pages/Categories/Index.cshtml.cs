using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;

public class Index : PageModel
{
    public IEnumerable<Category> Categories { get; set; } = null!;
    public IEnumerable<Category> CategoriesWithProduct { get; set; } = null!;
    
    public void OnGet()
    {
        Categories = new CategoryRepository().Get();
        CategoriesWithProduct = new CategoryRepository().GetCategoriesWithProducts();
    }

    
}