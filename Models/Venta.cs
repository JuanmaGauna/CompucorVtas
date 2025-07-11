using CompucorVtas.Models;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }

    public int ClienteId { get; set; }           // FK
    public Cliente Cliente { get; set; } = null!; // Navegación
}


