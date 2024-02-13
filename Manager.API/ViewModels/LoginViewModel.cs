using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O login não pode ser vazio.")]

        public string Login { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia")]

        public string Password { get; set; }
    }
}