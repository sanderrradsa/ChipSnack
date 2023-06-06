using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
    public class Categorie
    {
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string Naam { get; set; } = null!;

    }
}
