using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompucorVtas.Data;
using CompucorVtas.Models;

namespace CompucorVtas.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _context;

        public VentaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venta>> ObtenerTodos()
        {
            return await _context.Ventas.ToListAsync();
        }

        public async Task<Venta?> ObtenerPorId(int id)
        {
            return await _context.Ventas.FindAsync(id);
        }

        public async Task<Venta> Crear(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<Venta?> Actualizar(int id, Venta venta)
        {
            var existente = await _context.Ventas.FindAsync(id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(venta);
            await _context.SaveChangesAsync();
            return venta;
        }

        public async Task<bool> Eliminar(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null) return false;

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
