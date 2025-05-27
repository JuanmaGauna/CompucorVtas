using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.DTOs
{
    public class ProductoCreateDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public int CategoriaId { get; set; }
    }
}
