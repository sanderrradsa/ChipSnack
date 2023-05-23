using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int GebruikersId { get; set; }
        public int Aantal { get; set; }
        public string Opmerking { get; set; }
        public int Weeknr { get; set; }
        public int Jaar { get; set; }
        public int Herhalen { get; set; }
        public int SnackId { get; set; }
        public Snack Snack { get; set; } = null!;

    }
}
