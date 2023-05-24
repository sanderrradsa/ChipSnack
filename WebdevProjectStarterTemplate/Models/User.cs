using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required, MinLength(2), MaxLength(128)]
        public string Naam { get; set; } = null!;
        [Required, MinLength(2), MaxLength(128)]
        public string Email { get; set; } = null!;

        [Required, MinLength(2), MaxLength(128)]
        public string MicrosoftId { get; set; } = null!;

        [Required, MinLength(2), MaxLength(128)]
        public string Wachtwoord { get; set; } = null!;
        [Required]
        public int Rol { get; set; }

        public List<User> Users { get; set; }

    }
}
