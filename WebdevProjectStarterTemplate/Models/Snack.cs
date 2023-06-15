namespace WebdevProjectStarterTemplate.Models
{
    public class Snack
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        
        public string Beschrijving { get; set; }
        public int Prijs { get; set; }

        public Snackbar Snackbar { get; set; }

        public Category Categorie { get; set; }

    }
}
