using Microsoft.AspNetCore.Mvc;
using CompucorVtas.DTOs;
using CompucorVtas.Models;
using CompucorVtas.Services;
using AutoMapper;

namespace CompucorVtas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;

        public ProductosController(IProductoService productoService, IMapper mapper)
        {
            _productoService = productoService;
            _mapper = mapper;
        }

        // GET: api/v1/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            var productos = await _productoService.ObtenerTodos();
            var dto = _mapper.Map<List<ProductoDTO>>(productos);
            return Ok(dto);
        }

        // GET: api/v1/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await _productoService.ObtenerPorId(id);
            if (producto == null) return NotFound();

            var dto = _mapper.Map<ProductoDTO>(producto);
            return Ok(dto);
        }

        // GET: api/v1/productos/categoria/3
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetByCategoria(int categoriaId)
        {
            var productos = await _productoService.ObtenerTodos();
            var filtrados = productos
                .Where(p => p.CategoriaId == categoriaId)
                .ToList();

            if (!filtrados.Any())
                return NotFound($"No hay productos para la categoría {categoriaId}");

            var dto = _mapper.Map<List<ProductoDTO>>(filtrados);
            return Ok(dto);
        }

        // POST: api/v1/productos
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Post(ProductoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoProducto = _mapper.Map<Producto>(dto);
            var productoCreado = await _productoService.Crear(nuevoProducto);
            var dtoCreado = _mapper.Map<ProductoDTO>(productoCreado);

            return CreatedAtAction(nameof(Get), new { id = dtoCreado.Id }, dtoCreado);
        }

        // PUT: api/v1/productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest("El ID no coincide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _productoService.Actualizar(id, producto);
            if (actualizado == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/v1/productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _productoService.Eliminar(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
