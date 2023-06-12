using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Categories
{
    [Authorize]
    public class Create : PageModel
    {
        [BindProperty] public Categorie categorie { get; set; } = null!;
        public List<Categorie> ListCategorie = new List<Categorie>();
        public void OnGet()
        {
            GetCategorie();
        }
        public void GetCategorie()
        {
            var Categorie = new CategorieRepository().Get();
            foreach (var categorie in Categorie)
            {
                ListCategorie.Add(categorie);
            }
        }
        public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

            // Categorie = new Categorie();
            var createdCategorie = new CategorieRepository().Add(categorie);
        return RedirectToPage("Index");
    }

    public IActionResult OnPostCancel()
    {
        return Redirect(nameof(Index));
    }
    }
}