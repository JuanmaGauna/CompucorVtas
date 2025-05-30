using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.DTOs
{
    public class ClienteCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;
    }
}
