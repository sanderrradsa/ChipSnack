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
        public List<int> bestellingsSnackIds = new List<int>();

        AccountController ac = new AccountController();
        public bool isAdmin { get; set; }
        public int userId { get; set; }
        public int year { get; set; }
        public int week { get; set; }
        public IEnumerable<Snack> SelectedSnacks;
        public IEnumerable<Snackbar> SnackbarFilters;
        public IEnumerable<Categorie> CategoryFilters;

        public SnackReposiroty SnackRepository;
        public SnackbarRepository SnackbarRepository;
        public CategorieRepository CategorieRepository;

        public int selectedSnackbarId = -1;
        public int selectedCategoryId = -1;
        public void OnGet(int? snackbarID = null, int? categoryId = null)
        {
            userId = ac.GetID(User.Identity.Name);
            var dt = DateTime.Today;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) - 1;
            string date = DateTime.Now.ToString("yyyy");
            year = Convert.ToInt32(date);
            isAdmin = ac.IsAdmin(User.Identity.Name);

            SnackRepository = new SnackReposiroty();
            SnackbarRepository = new SnackbarRepository();
            CategorieRepository = new CategorieRepository();

            selectedSnackbarId = snackbarID ?? -1;
            selectedCategoryId = categoryId ?? -1;

            if (snackbarID is not null && categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromBarAndCategory((int)snackbarID, (int)categoryId);
            }
            else if (snackbarID is not null)
            {
                SelectedSnacks = SnackRepository.GetFromSnackBar((int)snackbarID);
            }
            else if (categoryId is not null)
            {
                SelectedSnacks = SnackRepository.GetFromCategory((int)categoryId);
            }
            else
            {
                SelectedSnacks = SnackRepository.Get();
            }
            
            SnackbarFilters = SnackbarRepository.Get();
            CategoryFilters = CategorieRepository.Get();
            AddedSnacks();
        }
        public IActionResult OnPostAdd(int snackId, string opmerking)
        {
            OnGet();
            var AddOrder = new BestellingRepository().Add(week, year, userId, snackId, opmerking);
            OnGet();

            return Page();

        }
        /// <summary>
        /// adds the snacks that are added in the mijn bestelling to a list.
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
