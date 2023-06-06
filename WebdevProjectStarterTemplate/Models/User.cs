namespace WebdevProjectStarterTemplate.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public int MicrosoftId { get; set; }
        public string Wachtwoord { get; set; } 
        public int Rol { get; set; }
    }
}
