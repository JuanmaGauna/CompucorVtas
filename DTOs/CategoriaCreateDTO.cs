using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.DTOs
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
