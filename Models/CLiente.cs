using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string? Telefono { get; set; }
    }
}