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
            var productos = await _productoService.ObtenerTodosIncluyendoCategoria();
            var dto = _mapper.Map<List<ProductoDTO>>(productos);
            return Ok(dto);
        }

        // GET: api/v1/productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await _productoService.ObtenerPorIdIncluyendoCategoria(id);
            if (producto == null) return NotFound();

            var dto = _mapper.Map<ProductoDTO>(producto);
            return Ok(dto);
        }

        // GET: api/v1/productos/categoria/3
        [HttpGet("categoria/{categoriaId}")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetByCategoria(int categoriaId)
        {
            var productos = await _productoService.ObtenerPorCategoriaIncluyendoCategoria(categoriaId);

            if (productos == null || !productos.Any())
                return NotFound($"No hay productos para la categoría {categoriaId}");

            var dto = _mapper.Map<List<ProductoDTO>>(productos);
            return Ok(dto);
        }

        // POST: api/v1/productos
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Post(ProductoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaExiste = await _productoService.CategoriaExiste(dto.CategoriaId);
            if (!categoriaExiste)
                return BadRequest($"No existe una categoría con ID {dto.CategoriaId}");

            var nuevoProducto = _mapper.Map<Producto>(dto);
            var productoCreado = await _productoService.Crear(nuevoProducto);

            var productoConCategoria = await _productoService.ObtenerPorIdIncluyendoCategoria(productoCreado.Id);
            var dtoCreado = _mapper.Map<ProductoDTO>(productoConCategoria);

            return CreatedAtAction(nameof(Get), new { id = dtoCreado.Id }, dtoCreado);
        }

        // PUT: api/v1/productos/5
        [HttpPut("{id}")]
public async Task<IActionResult> Put(int id, ProductoCreateDTO dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var categoriaExiste = await _productoService.CategoriaExiste(dto.CategoriaId);
    if (!categoriaExiste)
        return BadRequest($"No existe una categoría con ID {dto.CategoriaId}");

    var productoExistente = await _productoService.ObtenerPorId(id);
    if (productoExistente == null)
        return NotFound();

    var productoActualizado = _mapper.Map<Producto>(dto);
    productoActualizado.Id = id; // aseguramos el Id correcto

    var actualizado = await _productoService.Actualizar(id, productoActualizado);
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
