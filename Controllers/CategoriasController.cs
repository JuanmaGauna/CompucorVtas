using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucorVtas.Data;
using CompucorVtas.Models;
using CompucorVtas.DTOs;

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

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _context.Categorias
                .Select(c => new CategoriaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            var dto = new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };

            return Ok(dto);
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoria = new Categoria
            {
                Nombre = dto.Nombre
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            var categoriaDTO = new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };

            return CreatedAtAction(nameof(Get), new { id = categoria.Id }, categoriaDTO);
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
