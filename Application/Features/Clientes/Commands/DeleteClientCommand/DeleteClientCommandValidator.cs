using FluentValidation;


namespace Application.Features.Clientes.Commands.DeleteClientCommand
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropetyName} no puede ser vacio");
            
        }
    }
}
