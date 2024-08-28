using FluentValidation;


namespace Application.Features.Clientes.Commands.UpdateClientCommand
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

            RuleFor(p => p.Telefono)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio")
                .MaximumLength(80).WithMessage("{PropetyName} no debe exceder de {MaxLength}");

        }
    }
}
