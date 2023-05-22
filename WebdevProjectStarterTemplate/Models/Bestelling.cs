namespace WebdevProjectStarterTemplate.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int GebruikersId { get; set; }
        public int Aantal { get; set; }
        public string Opmerking { get; set; }
        public int weeknr { get; set; }
        public int jaar { get; set; }
        public int herhalen { get; set; }
        public int snackId { get;}
    }
}
