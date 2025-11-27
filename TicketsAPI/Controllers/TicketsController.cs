using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Models;
using TicketsAPI.Services;

namespace TicketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista paginada de tickets con filtros opcionales
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<Ticket>>> GetTickets(
            [FromQuery] TicketQueryParameters parameters)
        {
            try
            {
                _logger.LogInformation("Obteniendo tickets - Página: {PageNumber}, Tamaño: {PageSize}", 
                    parameters.PageNumber, parameters.PageSize);
                
                var result = await _ticketService.GetTicketsAsync(parameters);
                
                _logger.LogInformation("Tickets obtenidos exitosamente - Total: {Total}", result.TotalRecords);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tickets - Mensaje: {Message}, StackTrace: {StackTrace}", 
                    ex.Message, ex.StackTrace);
                
                return StatusCode(500, new { 
                    message = "Error al obtener los tickets", 
                    error = ex.Message,
                    innerError = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Obtiene un ticket por su ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketWithClientDTO>> GetTicket(string id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                
                if (ticket == null)
                {
                    return NotFound(new { message = $"Ticket con ID {id} no encontrado" });
                }

                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener ticket {id}");
                return StatusCode(500, new { message = "Error al obtener el ticket", error = ex.Message });
            }
        }
    }
}
