using System;
using System.ComponentModel.DataAnnotations;

namespace CompucorVtas.DTOs
{
    public class VentaCreateDTO
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un valor positivo.")]
        public decimal Total { get; set; }

        // Agregar más propiedades si la entidad Venta lo necesita.
    }
}
