using FluentValidation;
using CompucorVtas.Models;

namespace CompucorVtas.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .Length(1, 50).WithMessage("Máximo 50 caracteres");
        }
    }
}
