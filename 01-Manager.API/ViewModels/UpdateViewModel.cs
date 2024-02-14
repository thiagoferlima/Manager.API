using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = "O Id não pode ser vazio.")]
        [Range(1, int.MaxValue, ErrorMessage = "O Id não pode ser menor que 1.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "o Nome não pode ser vazio.")]
        [MinLength(3, ErrorMessage = "O nome deveter no minino 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no máximo 80 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "o email não pode ser vazio.")]
        [MinLength(10, ErrorMessage = "O email deveter no minino 10 caracteres.")]
        [MaxLength(180, ErrorMessage = "O email deve conter no máximo 180 caracteres.")]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+", ErrorMessage = "o email informado não é valido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha não pode ser vazio.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minino 6 caracteres.")]
        [MaxLength(80, ErrorMessage = "A senha deve conter no máximo 80 caracteres.")]
        public string Password { get; set; }


    }
}