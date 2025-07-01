using CompucorVtas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompucorVtas.Services
{
    public interface IProductoService
    {
          Task<List<Producto>> ObtenerTodosIncluyendoCategoria();
    Task<Producto?> ObtenerPorIdIncluyendoCategoria(int id);
    Task<List<Producto>> ObtenerPorCategoriaIncluyendoCategoria(int categoriaId);
    Task<bool> CategoriaExiste(int categoriaId);
    Task<Producto> Crear(Producto producto);
    Task<Producto?> Actualizar(int id, Producto producto);
    Task<bool> Eliminar(int id);
    Task<Producto?> ObtenerPorId(int id);
    }
}
