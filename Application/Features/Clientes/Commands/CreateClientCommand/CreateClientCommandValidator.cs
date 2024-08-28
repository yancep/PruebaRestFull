using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator: AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator() {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");
        }
    }
}
