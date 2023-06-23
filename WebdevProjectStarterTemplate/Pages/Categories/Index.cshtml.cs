using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories
{
    [Authorize]
    public class Index : PageModel
    {
        /// <summary>Alle categoriën, om te laten zien in het overzicht</summary>
        public IEnumerable<Categorie> Categories { get; set; } = null!;

        public void OnGet()
        {
            Categories = new CategorieRepository().Get();
        }
    }
}
