using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebdevProjectStarterTemplate.Models;
using WebdevProjectStarterTemplate.Repositories;

namespace WebdevProjectStarterTemplate.Pages.Login
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public User User { get; set; }
        public UsersRepository usersRepository = new UsersRepository();
        public void OnGet(int UserId)
        {
            usersRepository.GetIe();

        }
        public void OnPost()
        {

            /*string email = /*new UsersRepository().Users[0].Email; new UsersRepository().GetIe().ToString();
            var test = new UsersRepository().Users[0].Email;
            Credential.userName = email;*/

            foreach (var user in User.Users) {
                if (Credential.userName == user.Email && Credential.passWord == User.Wachtwoord)
                {
                    throw new Exception("PIEMEL");
                }
                else
                {

                }
            }

        }
    }
    public class Credential
    {
        [Required]
        [Display(Name = "E-mail")]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string passWord { get; set; }
        

    }
}
