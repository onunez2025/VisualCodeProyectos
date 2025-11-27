using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Models;
using TicketsAPI.Services;

namespace TicketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(IClienteService clienteService, ILogger<ClientesController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Cliente>>> GetClientes([FromQuery] ClienteQueryParameters parameters)
        {
            try
            {
                var result = await _clienteService.GetClientesAsync(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes");
                return StatusCode(500, new { message = "Error al obtener clientes", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(string id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);
                
                if (cliente == null)
                {
                    return NotFound(new { message = $"Cliente con ID {id} no encontrado" });
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener cliente con ID {id}");
                return StatusCode(500, new { message = "Error al obtener cliente", error = ex.Message });
            }
        }
    }
}
