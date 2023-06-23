using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class Update : PageModel
    {
        [BindProperty]
        public Snack snack { get; set; } = null!;
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategorie = new List<Categorie>();
        //public SelectList Categorieen { get; set; }
        //public async Task<IActionResult> OnGetAsync(int snackId)
        //{
        //    Snack = await SnackRepository.GetByIdAsync(snackId);

        //    if (Snack == null)
        //    {
        //        return NotFound();
        //    }

        //    Categorieen = new SelectList(await CategorieRepository.GetAllAsync(), "Id", "Naam", Snack.CategorieID);

        //    return Page();
        //}

        /// <summary>
        /// Verwerkt het GET-verzoek voor het bewerken van een snack.
        /// </summary>
        /// <param name="snackId">ID van de snack die wordt bewerkt</param>
        public void OnGet(int snackId)
        {
            snack = new SnackRepository().Get(snackId); // Haal de snack op met het opgegeven ID
            GetSnackbars(); // Haal de snackbars op
            GetCategorie(); // Haal de categorieën op
        }

        /// <summary>
        /// Haalt de lijst met snackbars op en voegt deze toe aan de lijst.
        /// </summary>
        public void GetSnackbars()
        {
            var Snackbars = new SnackbarRepository().Get(); // Haal de snackbars op
            foreach (var snackbar in Snackbars)
            {
                ListSnackBars.Add(snackbar); // Voeg elke snackbar toe aan de lijst
            }
        }

        /// <summary>
        /// Haalt de lijst met categorieën op en voegt deze toe aan de lijst.
        /// </summary>
        public void GetCategorie()
        {
            var Categorie = new CategorieRepository().Get(); // Haal de categorieën op
            foreach (var categorie in Categorie)
            {
                ListCategorie.Add(categorie); // Voeg elke categorie toe aan de lijst
            }
        }

        /// <summary>
        /// Verwerkt het POST-verzoek voor het bijwerken van een snack.
        /// </summary>
        /// <param name="snack">Bijgewerkte snackgegevens</param>
        /// <returns>De pagina van de snackbars</returns>
        public IActionResult OnPost(Snack snack)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("~/Snackbars/Index"); // Als de modelvalidatie mislukt, doorverwijzen naar de snackbars pagina
            }

            var updatedSnack = new SnackRepository().Update(snack); // Bijwerken van de snackgegevens

            return Redirect("~/Snackbars/Index");
        }


        /*public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index));
        }*/
    }
}
