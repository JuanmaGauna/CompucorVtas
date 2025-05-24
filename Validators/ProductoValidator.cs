using FluentValidation;
using CompucorVtas.Models;

namespace CompucorVtas.Validators
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .Length(1, 100).WithMessage("Máximo 100 caracteres");
            RuleFor(p => p.Descripcion)
                .Length(0, 250).WithMessage("Máximo 250 caracteres");
            RuleFor(p => p.Precio)
                .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser un valor positivo");
            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}

                