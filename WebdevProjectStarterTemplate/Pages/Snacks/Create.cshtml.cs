using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
