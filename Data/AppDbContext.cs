using Microsoft.EntityFrameworkCore;
using CompucorVtas.Models;



namespace CompucorVtas.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }      
        
    }
}