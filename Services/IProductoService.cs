using CompucorVtas.Models;

namespace CompucorVtas.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerTodos();
        Task<Producto?> ObtenerPorId(int id);
        Task<Producto> Crear(Producto producto);
        Task<bool> Eliminar(int id);
        Task<Producto?> Actualizar(int id, Producto producto);
    }
}
