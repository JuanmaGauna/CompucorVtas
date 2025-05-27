using Microsoft.AspNetCore.Mvc;
using CompucorVtas.Data;
using CompucorVtas.Models;
using Microsoft.EntityFrameworkCore;
using CompucorVtas.DTOs;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
{
    var productos = await _context.Productos
        .Include(p => p.Categoria)
        .Select(p => new ProductoDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Precio = p.Precio,
            Categoria = p.Categoria != null ? p.Categoria.Nombre : "(Sin categoría)"
        })
        .ToListAsync();

    return productos;
}


        // GET: api/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return producto;
        }
[HttpGet("categoria/{categoriaId}")]
public async Task<ActionResult<IEnumerable<Producto>>> GetByCategoria(int categoriaId)
{
    var productos = await _context.Productos
        .Where(p => p.CategoriaId == categoriaId)
        .Include(p => p.Categoria)
        .ToListAsync();

    if (productos == null || productos.Count == 0)
        return NotFound($"No hay productos para la categoría {categoriaId}");

    return productos;
}

        // POST: api/productos
[HttpPost]
public async Task<ActionResult<ProductoDTO>> Post(ProductoCreateDTO dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var producto = new Producto
    {
        Nombre = dto.Nombre,
        Descripcion = dto.Descripcion,
        Precio = dto.Precio,
        Stock = dto.Stock,
        CategoriaId = dto.CategoriaId
    };

    _context.Productos.Add(producto);
    await _context.SaveChangesAsync();

    // Cargar la categoría (solo el nombre) para el DTO
    var categoria = await _context.Categorias.FindAsync(dto.CategoriaId);

    var productoDTO = new ProductoDTO
    {
        Id = producto.Id,
        Nombre = producto.Nombre,
        Precio = producto.Precio,
        Categoria = categoria?.Nombre ?? "(Sin categoría)"
    };

    return CreatedAtAction(nameof(Get), new { id = producto.Id }, productoDTO);
}



   [HttpPut("{id}")]
public async Task<IActionResult> Put(int id, Producto producto)
{
    if (id != producto.Id)
        return BadRequest("El ID no coincide.");

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    _context.Entry(producto).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_context.Productos.Any(e => e.Id == id))
            return NotFound();
        else
            throw;
    }

    return NoContent();
}

        // DELETE: api/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

