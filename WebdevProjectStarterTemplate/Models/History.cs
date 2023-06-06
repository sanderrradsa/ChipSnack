using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models;

public class History
{
    public Snack Snack = null!;

    public int jaar;

    public int weeknr;
    
    public int Totaal;
}