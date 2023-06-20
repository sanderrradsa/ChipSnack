using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Snackbars
{
    [Authorize]
    public class Update : PageModel
    {
        public Snackbar Snackbar { get; set; } = null!;
        AccountController accountController = new AccountController();
        // Handler voor GET-verzoek bij het ophalen van de Snackbar voor bewerking
        public void OnGet(int snackbarId)
        {
            Snackbar = new SnackbarRepository().Get(snackbarId);
            GetSnacks(snackbarId);
            GetCategorie();
        }
        // Handler voor POST-verzoek bij het bijwerken van de Snackbar
        public IActionResult OnPostSnackbar(Snackbar snackbar)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedSnackbar = new SnackbarRepository().Update(snackbar);

            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina na succesvol bijwerken
        }
        // Handler voor POST-verzoek bij annuleren
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina bij annuleren
        }
        [BindProperty]
        public Snack snack { get; set; } = null!;
        public List<Snack> ListSnacks = new List<Snack>();
        public List<Snackbar> ListSnackBars = new List<Snackbar>();
        public List<Categorie> ListCategorie = new List<Categorie>();
        // Methode om de gegevens van een specifieke Snack op te halen voor bewerking
        public void GetSnackUpdate(int snackId)
        {
            snack = new SnackReposiroty().Get(snackId);
            GetCategorie();
        }
        // Methode om de Snacks van een specifieke Snackbar op te halen
        public void GetSnacks(int snackbarId)
        {
            var Snack = new SnackReposiroty().Get();
            foreach (var snack in Snack)
            {
                if (snack.Snackbarid == snackbarId)
                {
                    ListSnacks.Add(snack);
                }
            }
        }
        // Methode om de Categorieën op te halen
        public void GetCategorie()
        {
            var Categorie = new CategorieRepository().Get();
            foreach (var categorie in Categorie)
            {
                ListCategorie.Add(categorie);
            }
        }
        // Handler voor POST-verzoek bij het bijwerken van een Snack
        public IActionResult OnPostSnack(Snack snack)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var updatedSnack = new SnackReposiroty().Update(snack);

            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina na succesvol bijwerken
        }
        // Handler voor POST-verzoek bij annuleren
        public IActionResult OnPostCancel1()
        {
            return RedirectToPage(nameof(Index)); // Doorverwijzen naar de indexpagina bij annuleren
        }
    }
}

