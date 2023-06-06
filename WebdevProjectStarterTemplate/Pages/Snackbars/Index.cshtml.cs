using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Index : PageModel
    {

        public IEnumerable<Snackbar> Snackbars { get; set; } = null!;
        public void OnGet()
        {
            Snackbars = new SnackbarRepository().Get();
        }
    }
}
