using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Winkelwagen
{
    [Authorize]
    public class Index : PageModel
    {
        public IEnumerable<Bestelling> Bestelling { get; set; } = null!;
        public IEnumerable<Budget> budgets { get; set; } = null!;

        public int year { get; set; }
        public int week { get; set; }
        public void OnGet()
        {
            var dt = DateTime.Today;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) -1;
            string date = DateTime.Now.ToString("yyyy");
            year = Convert.ToInt32(date);
            Bestelling = new BestellingRepository().GetBestellingWithSnack(year, week);
            budgets = new BudgetRepository().Get();

        }

        public IActionResult OnPostIncrement(int bestellingId)
        {
                var updatedOrder = new BestellingRepository().Update(1, bestellingId);
            OnGet();

            return Page();

        }
        public IActionResult OnPostDecrement(int bestellingId, int aantal)
        {
            if (aantal > 1)
            {
                var updatedOrder = new BestellingRepository().Update(-1, bestellingId);
            }
            OnGet();
            return Page();

        }
        public IActionResult OnPostLockInOrder()
        {
            OnGet();
            var updatedOrder = new BestellingRepository().UpdateLockIn(year, week);
            OnGet();
            return Page();

        }

    

    }
}
