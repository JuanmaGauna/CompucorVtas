using CompucorVtas.Models;
using CompucorVtas.Data;
using Microsoft.EntityFrameworkCore;

namespace CompucorVtas.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorId(int id)
        {
            return await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> Crear(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            await _context.Entry(producto).Reference(p => p.Categoria).LoadAsync();

            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Producto?> Actualizar(int id, Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(id);
            if (productoExistente == null) return null;

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.CategoriaId = producto.CategoriaId;

            await _context.SaveChangesAsync();
            
             // Cargar la categoría actualizada
            await _context.Entry(productoExistente).Reference(p => p.Categoria).LoadAsync();

            return productoExistente;
        }
    }
}
