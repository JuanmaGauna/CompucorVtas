using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompucorVtas.DTOs;
using CompucorVtas.Models;
using CompucorVtas.Services;
using AutoMapper;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;

        public VentasController(IVentaService ventaService, IMapper mapper)
        {
            _ventaService = ventaService;
            _mapper = mapper;
        }

        // GET: api/v1/ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var ventas = await _ventaService.ObtenerTodos();
            var dto = _mapper.Map<List<VentaDTO>>(ventas);
            return Ok(dto);
        }

        // GET: api/v1/ventas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            var venta = await _ventaService.ObtenerPorId(id);
            if (venta == null)
                return NotFound();

            var dto = _mapper.Map<VentaDTO>(venta);
            return Ok(dto);
        }

        // POST: api/v1/ventas
        [HttpPost]
        public async Task<ActionResult<VentaDTO>> Post(VentaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaVenta = _mapper.Map<Venta>(dto);
            var ventaCreada = await _ventaService.Crear(nuevaVenta);

            var dtoCreado = _mapper.Map<VentaDTO>(ventaCreada);
            return CreatedAtAction(nameof(Get), new { id = dtoCreado.Id }, dtoCreado);
        }

        // PUT: api/v1/ventas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VentaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ventaExistente = await _ventaService.ObtenerPorId(id);
            if (ventaExistente == null)
                return NotFound();

            var ventaActualizada = _mapper.Map<Venta>(dto);
            ventaActualizada.Id = id;

            var actualizado = await _ventaService.Actualizar(id, ventaActualizada);
            if (actualizado == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/v1/ventas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _ventaService.Eliminar(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
