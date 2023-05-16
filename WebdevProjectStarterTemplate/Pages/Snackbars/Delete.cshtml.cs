using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    public class Delete : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;

        public void OnGet([FromRoute] int snackbarId)
        {
            Snackbar = new SnackbarRepository().Get(snackbarId);
        }

        public IActionResult OnPostDelete([FromRoute] int snackbarId)
        {
            bool success = new SnackbarRepository().Delete(snackbarId);
            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}
