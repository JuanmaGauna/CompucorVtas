using System.Collections.Generic;
using System.Threading.Tasks;
using CompucorVtas.Models;

namespace CompucorVtas.Services
{
    public interface IVentaService
    {
        Task<List<Venta>> ObtenerTodos();
        Task<Venta?> ObtenerPorId(int id);
        Task<Venta> Crear(Venta venta);
        Task<Venta?> Actualizar(int id, Venta venta);
        Task<bool> Eliminar(int id);
    }
}
