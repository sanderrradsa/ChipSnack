using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;

public class Delete : PageModel
{
    public Category Category { get; set; } = null!;
    
    public void OnGet([FromRoute] int categoryId)
    {
        Category = new CategoryRepository().Get(categoryId);
    }

    public IActionResult OnPostDelete([FromRoute]int categoryId)
    {
        bool success = new CategoryRepository().Delete(categoryId);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return RedirectToPage(nameof(Index));
    }
}