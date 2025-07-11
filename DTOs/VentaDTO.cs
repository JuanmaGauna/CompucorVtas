public class VentaDTO
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string ClienteNombre { get; set; } = string.Empty;
}
