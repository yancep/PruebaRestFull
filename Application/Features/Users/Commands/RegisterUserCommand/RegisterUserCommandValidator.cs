using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUserCommand
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

            RuleFor(p => p.UserName)
                    .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

            RuleFor(p => p.Password)
                    .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                    .MaximumLength(15).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

            RuleFor(p => p.ConfirmPassword)
                    .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                    .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}")
                    .Equal(p => p.Password).WithMessage("{PropetyName} debe ser igual a Pasword");

        }
        
    }
    
}
