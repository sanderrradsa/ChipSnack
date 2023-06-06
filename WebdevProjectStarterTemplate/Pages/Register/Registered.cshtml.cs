using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebdevProjectStarterTemplate.Pages.Register
{
    [Authorize(Roles = "admin")]
    public class RegisteredModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
}
