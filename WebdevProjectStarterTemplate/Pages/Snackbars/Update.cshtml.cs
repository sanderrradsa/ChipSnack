using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Update : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;

        public void OnGet(int snackbarId)
        {
            Snackbar = new SnackbarRepository().Get(snackbarId);
        }

        public IActionResult OnPost(Snackbar snackbar)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedSnackbar = new SnackbarRepository().Update(snackbar);

            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}
