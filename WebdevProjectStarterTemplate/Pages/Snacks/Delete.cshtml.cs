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
        // Handler voor GET-verzoek
        public void OnGet([FromRoute] int snackId)
        {
            Snack = new SnackReposiroty().Get(snackId); // Haal de snack op met het opgegeven snackId
        }
        // Handler voor POST-verzoek om de snack te verwijderen
        public IActionResult OnPostDelete([FromRoute] int snackId)
        {
            bool success = new SnackReposiroty().Delete(snackId); // Verwijder de snack met het opgegeven snackId
            return Redirect("~/Snackbars/Index"); // Doorverwijzen naar de indexpagina van Snackbars na het succesvol verwijderen van de snack
        }
    }

    /*public IActionResult OnPostCancel()
    {
        //return redirecttopre(nameof(Index));
    }*/
}

