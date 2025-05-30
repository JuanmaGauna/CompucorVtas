using FluentValidation;
using CompucorVtas.DTOs;

namespace CompucorVtas.Validators
{
    public class ClienteCreateDTOValidator : AbstractValidator<ClienteCreateDTO>
    {
        public ClienteCreateDTOValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("Máximo 100 caracteres");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El email no es válido");

            RuleFor(c => c.Telefono)
                .MaximumLength(20).WithMessage("Máximo 20 caracteres");
        }
    }
}
