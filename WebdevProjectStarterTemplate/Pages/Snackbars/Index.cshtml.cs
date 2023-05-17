using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{

    public class IndexModel : PageModel
    {

        public IEnumerable<Snackbar> Snackbars { get; set; } = null!;
        public void OnGet()
        {
            Snackbars = new SnackbarRepository().Get();
        }
    }
}