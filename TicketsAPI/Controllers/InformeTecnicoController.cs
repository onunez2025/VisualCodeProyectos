using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Models;

namespace TicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeTecnicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InformeTecnicoController> _logger;

        public InformeTecnicoController(ApplicationDbContext context, ILogger<InformeTecnicoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<InformeTecnico>> GetInformeByTicket(string ticketId)
        {
            try
            {
                var informe = await _context.InformesTecnicos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.Ticket == ticketId);

                if (informe == null)
                {
                    return NotFound(new { message = "No se encontró informe técnico para este ticket" });
                }

                return Ok(informe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener informe técnico del ticket {TicketId}", ticketId);
                return StatusCode(500, new { error = "Error al obtener informe técnico", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InformeTecnico>> GetInformeById(string id)
        {
            try
            {
                var informe = await _context.InformesTecnicos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.IdInformeTecnico == id);

                if (informe == null)
                {
                    return NotFound(new { message = "Informe técnico no encontrado" });
                }

                return Ok(informe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener informe técnico {Id}", id);
                return StatusCode(500, new { error = "Error al obtener informe técnico", details = ex.Message });
            }
        }
    }
}
