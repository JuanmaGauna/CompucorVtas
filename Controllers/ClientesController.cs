using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucorVtas.Data;
using CompucorVtas.Models;
using CompucorVtas.DTOs;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            var clientes = await _context.Clientes
                .Select(c => new ClienteDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Telefono = c.Telefono
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return Ok(dto);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Post(ClienteCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, clienteDTO);
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
