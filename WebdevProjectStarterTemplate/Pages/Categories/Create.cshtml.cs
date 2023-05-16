using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories;

public class Create : PageModel
{
    [BindProperty] public Category Category { get; set; } = null!;
    
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var createdCategory = new CategoryRepository().Add(Category);
        return RedirectToPage(nameof(Index));
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
}