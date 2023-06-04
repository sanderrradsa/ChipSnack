using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Winkelwagen;

public class Create : PageModel
{
    public int year { get; set; }
    public int week { get; set; }
    public void OnGet(int snackId, int favorite=0, string opmerking="")
    {
        if (favorite == 1)
        {
            new BestellingRepository().AddFavorite(snackId, 1, opmerking); // TODO: gebruiker is niet altijd 1
            return;
        }
        var dt = DateTime.Today;
        Calendar cal = new CultureInfo("en-US").Calendar;
        week = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday) -1;
        string date = DateTime.Now.ToString("yyyy");
        year = Convert.ToInt32(date); 
        new BestellingRepository().Add(snackId, week, year, 1, opmerking); // TODO: gebruiker is niet altijd 1
    }
}