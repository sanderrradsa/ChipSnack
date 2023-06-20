using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Bcpg;
using System.Globalization;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;


namespace WebdevProjectStarterTemplate.Pages.Winkelwagen
{
    [Authorize]
    public class Index : PageModel
    {
        public IEnumerable<Bestelling> Bestelling { get; set; } = null!;
        public int budgets { get; set; } = 0;

        public int TotalOrderValue { get; set; } = 0;

        AccountController ac = new AccountController();

        public int userId { get; set; }

        public int year { get; set; }
        public int week { get; set; }
        public void OnGet()
        {
            userId = ac.GetID(User.Identity.Name);
            var dt = DateTime.Today;
            Calendar cal = new CultureInfo("en-US").Calendar;
            week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) -1;
            string date = DateTime.Now.ToString("yyyy");
            year = Convert.ToInt32(date);
            Bestelling = new BestellingRepository().GetBestellingWithSnack(year, week, userId);
            var getbudgets = new BudgetRepository().Get();
            budgets = Convert.ToInt32(getbudgets.BudgetMax);
            GetTotalOrderValue();
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
            var updatedOrder = new BestellingRepository().UpdateLockIn(year, week, userId);
            OnGet();
            return Page();

        }

        public void GetTotalOrderValue()
        {
            TotalOrderValue = 0;
            foreach (Bestelling bestelling in Bestelling)
            {
                TotalOrderValue += bestelling.Snack.Prijs * bestelling.Aantal;
            }
        }
        
        public IActionResult OnPostAdd(int snackId, string opmerking)
        {
            OnGet();
            var AddOrder = new BestellingRepository().Add(week, year, userId, snackId, opmerking);
            OnGet();

            return Page();

        }
        
        
    }
}
