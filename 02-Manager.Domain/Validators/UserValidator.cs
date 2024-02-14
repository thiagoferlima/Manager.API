using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Manager.Domain.Entities;

namespace Manager.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia")

                .NotNull()
                .WithMessage("A entidade não pode ser nula");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome não pode ser vazio")

                .NotNull()
                .WithMessage("O nome não pode ser nulo")

                .MinimumLength(3)
                .WithMessage("O nome deveter no minino 3 caracteres")

                .MaximumLength(80)
                .WithMessage("O nome deve conter no máximo 80 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ser vazio")

                .NotNull()
                .WithMessage("A senha não pode ser nulo")

                .MinimumLength(6)
                .WithMessage("A senha deve ter no minino 3 caracteres")

                .MaximumLength(80)
                .WithMessage("A senha deve ter no máximo 80 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email não pode ser vazio")

                .NotNull()
                .WithMessage("O email não pode ser nulo")

                .MinimumLength(10)
                .WithMessage("O email deveter no minino 10 caracteres")

                .MaximumLength(180)
                .WithMessage("O email deve conter no máximo 180 caracteres")

                .Matches(@"^[\w\.-]+@[\w\.-]+\.\w+")
                .WithMessage("O email informado não é válido");


        }
    }

}