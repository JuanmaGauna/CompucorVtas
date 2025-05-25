using Microsoft.AspNetCore.Mvc;
using CompucorVtas.Data;
using CompucorVtas.Models;
using Microsoft.EntityFrameworkCore;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await _context.Productos
            .Include(p => p.Categoria)
            .ToListAsync();
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
public async Task<ActionResult<Producto>> Post(Producto producto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    _context.Productos.Add(producto);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
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

