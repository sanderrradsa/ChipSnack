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

        public int year { get; set; }
        public int week { get; set; }
        public void OnGet()
        {
            var dt = DateTime.Today;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) -1;
            string date = DateTime.Now.ToString("yyyy");
            year = Convert.ToInt32(date);
            Bestelling = new BestellingRepository().GetBestellingWithSnack(1, year, week);
        }

        public IActionResult OnPostIncrement(int bestellingId)
        {
                var updatedOrder = new BestellingRepository().Update(1, bestellingId);
            OnGet();

            return Page();

        }
        public IActionResult OnPostDecrement(int bestellingId)
        {
            var updatedOrder = new BestellingRepository().Update(-1, bestellingId);
            OnGet();

            return Page();

        }
    }
}
