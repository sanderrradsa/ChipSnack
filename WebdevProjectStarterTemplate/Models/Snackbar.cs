using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
    public class Snackbar
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Naam { get; set; }
    }
}
