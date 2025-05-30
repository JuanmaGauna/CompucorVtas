using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Máximo 250 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

    }
}
