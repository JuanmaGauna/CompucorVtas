using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string? Descripcion { get; set; }

        // Relación: una categoría tiene muchos productos
        public List<Producto> Productos { get; set; } = new();
    }
}
