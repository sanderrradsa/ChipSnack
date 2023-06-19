using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace WebdevProjectStarterTemplate.Pages.WachtwoordVergeten
{
    [Authorize(Roles = "admin")]
    public class SuccesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
