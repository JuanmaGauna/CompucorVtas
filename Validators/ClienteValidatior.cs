using FluentValidation;
using CompucorVtas.Models;

namespace CompucorVtas.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .Length(1, 100).WithMessage("Máximo 100 caracteres");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(c => c.Telefono)
                .Matches(@"^\+?\d{7,15}$").WithMessage("El teléfono debe contener entre 7 y 15 dígitos, opcionalmente con '+'");
        }
    }
}
