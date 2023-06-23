using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace WebdevProjectStarterTemplate.Pages.Snacks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<int> bestellingsSnackIds = new List<int>(); // Lijst om de snack-ids van bestellingen bij te houden

        AccountController ac = new AccountController();
        public bool isAdmin { get; set; } // Geeft aan of de gebruiker een beheerder is
        public int userId { get; set; } // ID van de ingelogde gebruiker
        public int year { get; set; } // Huidig jaar
        public int week { get; set; } // Huidige week
        public IEnumerable<Snack> SelectedSnacks; // Geselecteerde snacks
        public IEnumerable<Snackbar> SnackbarFilters; // Snackbar filters
        public IEnumerable<Categorie> CategoryFilters; // Categorie filters

        public SnackRepository SnackRepository; // Repository voor snacks
        public SnackbarRepository SnackbarRepository; // Repository voor snackbars
        public CategorieRepository CategorieRepository; // Repository voor categorieën

        public int selectedSnackbarId = -1; // Geselecteerde snackbar ID
        public int selectedCategoryId = -1; // Geselecteerde categorie ID

        // Handler voor GET-verzoek
        public void OnGet(int? snackbarID = null, int? categoryId = null)
        {
            userId = ac.GetID(User.Identity.Name); // Haal ID van ingelogde gebruiker op
            var dt = DateTime.Today;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) - 1; // Bereken huidige week
            string date = DateTime.Now.ToString("yyyy");
            year = Convert.ToInt32(date); // Huidig jaar
            isAdmin = ac.IsAdmin(User.Identity.Name); // Controleer of gebruiker een beheerder is
            SnackRepository = new SnackRepository();
            SnackbarRepository = new SnackbarRepository();
            CategorieRepository = new CategorieRepository();
            selectedSnackbarId = snackbarID ?? -1; // Wijs geselecteerde snackbar ID toe, indien aanwezig
            selectedCategoryId = categoryId ?? -1; // Wijs geselecteerde categorie ID toe, indien aanwezig
            if (snackbarID is not null && categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromBarAndCategory((int)snackbarID, (int)categoryId); // Haal snacks op van geselecteerde snackbar en categorie
            }
            else if (snackbarID is not null)
            {
                SelectedSnacks = SnackRepository.GetFromSnackBar((int)snackbarID); // Haal snacks op van geselecteerde snackbar
            }
            else if (categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromCategory((int)categoryId); // Haal snacks op van geselecteerde categorie
            }
            else
            {
                SelectedSnacks = SnackRepository.Get(); // Haal alle snacks op
            }
            SnackbarFilters = SnackbarRepository.Get(); // Haal snackbars op voor filteropties
            CategoryFilters = CategorieRepository.Get(); // Haal categorieën op voor filteropties
            AddedSnacks(); // Voeg de snacks toe die zijn toegevoegd aan "Mijn bestelling" aan de lijst
        }
        /// <summary>
        /// Verwerkt het POST-verzoek om een snack aan de bestelling toe te voegen.
        /// </summary>
        /// <param name="snackId">ID van de snack die wordt toegevoegd</param>
        /// <param name="opmerking">Optionele opmerking bij de bestelling</param>
        /// <returns>De pagina</returns>
        public IActionResult OnPostAdd(int snackId, string opmerking)
        {
            OnGet(); // Vernieuw de gegevens op de pagina
            var AddOrder = new BestellingRepository().Add(week, year, userId, snackId, opmerking); // Voeg snack toe aan de bestelling
            OnGet(); // Vernieuw de gegevens op de pagina

            return Page(); // Laad de pagina opnieuw
        }


        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("/Winkelwagen/Index");
        }
        /// <summary>
        /// voegt de snacks die zijn toegevoegd in mijn bestelling toe aan een lijst.
        /// </summary>
        public void AddedSnacks()
        {
            bestellingsSnackIds.Clear();
            var snackIds = new BestellingRepository().GetSnackIds(week, year, userId);
            //Todo moet de bestellingsSnackIds lijst vullen 
            foreach (var snackId in snackIds)
            {
                bestellingsSnackIds.Add(snackId);
            }
        }

    }
}
