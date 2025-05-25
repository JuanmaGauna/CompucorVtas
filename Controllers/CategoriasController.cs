using Microsoft.AspNetCore.Mvc;
using CompucorVtas.Models;
using CompucorVtas.Data;
using Microsoft.EntityFrameworkCore;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            return await _context.Categorias.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();
            return categoria;
        }
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = categoria.Id }, categoria);
        }
    }
}