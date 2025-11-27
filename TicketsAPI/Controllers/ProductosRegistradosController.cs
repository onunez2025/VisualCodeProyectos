using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Models;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosRegistradosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductosRegistradosController> _logger;

        public ProductosRegistradosController(ApplicationDbContext context, ILogger<ProductosRegistradosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<List<ProductoRegistradoWithMaterialDTO>>> GetProductosByCliente(string clienteId)
        {
            try
            {
                var productos = await (from pr in _context.ProductosRegistrados.AsNoTracking()
                                      join material in _context.Materiales.AsNoTracking()
                                      on pr.Producto equals material.IdMaterial into materialGroup
                                      from material in materialGroup.DefaultIfEmpty()
                                      where pr.Cliente == clienteId
                                      select new ProductoRegistradoWithMaterialDTO
                                      {
                                          Id = pr.Id,
                                          Cliente = pr.Cliente,
                                          Producto = pr.Producto,
                                          CategoriaProductoRegistrado = pr.CategoriaProductoRegistrado,
                                          Departamento = pr.Departamento,
                                          Provincia = pr.Provincia,
                                          Distrito = pr.Distrito,
                                          EdificioPlantaSala = pr.EdificioPlantaSala,
                                          NombreVia = pr.NombreVia,
                                          Numero = pr.Numero,
                                          LugarCompra = pr.LugarCompra,
                                          Zona = pr.Zona,
                                          NombreZona = pr.NombreZona,
                                          Referencia = pr.Referencia,
                                          Garantia = pr.Garantia,
                                          Latitud = pr.Latitud,
                                          Longitud = pr.Longitud,
                                          CreadoEl = pr.CreadoEl,
                                          CreadoPor = pr.CreadoPor,
                                          ModificadoEl = pr.ModificadoEl,
                                          ModificadoPor = pr.ModificadoPor,
                                          
                                          // Datos del Material
                                          MaterialLabel = material != null 
                                              ? "[" + (material.IdExterno ?? "") + "] " + (material.Nombre ?? "")
                                              : null,
                                          MaterialNombre = material != null ? material.Nombre : null,
                                          MaterialCategoria = material != null ? material.Categoria : null,
                                          MaterialIdExterno = material != null ? material.IdExterno : null
                                      }).ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos registrados del cliente {ClienteId}", clienteId);
                return StatusCode(500, new { error = "Error al obtener productos registrados", details = ex.Message });
            }
        }
    }
}
