using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    public class CreateModel : PageModel
    {
        [BindProperty] public Snackbar Snackbar { get; set; } = null!;
    
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
        
            var createdCategorie = new SnackbarRepository().Add(Snackbar);
            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return Redirect(nameof(Index));
        }
    }
}
