using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class Delete : PageModel
    {
        public Snack Snack { get; set; } = null!;

        public void OnGet([FromRoute] int snackId)
        {
            Snack = new SnackReposiroty().Get(snackId);
        }

        public IActionResult OnPostDelete([FromRoute] int snackId)
        {
            bool success = new SnackReposiroty().Delete(snackId);
            return RedirectToPage(nameof(Index));
        }

        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }
    }
}
