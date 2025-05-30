using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.DTOs
{
    public class ProductoCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "La descripción no puede superar 250 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Debe indicar la categoría")]
        public int CategoriaId { get; set; }
    }
}
