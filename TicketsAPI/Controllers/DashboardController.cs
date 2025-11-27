using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Models;

namespace TicketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStats>> GetDashboardStats()
        {
            try
            {
                var now = DateTime.Now;
                var inicioMes = new DateTime(now.Year, now.Month, 1);
                var inicioDia = now.Date;
                var hace7Dias = now.AddDays(-7);

                // Total de tickets
                var totalTickets = await _context.Tickets.CountAsync();

                // Tickets por estado
                var ticketsPorEstado = await _context.Tickets
                    .GroupBy(t => t.Estado)
                    .Select(g => new EstadoStats
                    {
                        Estado = g.Key ?? "Sin estado",
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .ToListAsync();

                // Conteos por estado específico
                var ticketsPendientes = ticketsPorEstado
                    .Where(x => x.Estado.Contains("Pendiente") || x.Estado.Contains("Aceptado"))
                    .Sum(x => x.Cantidad);

                var ticketsEnProceso = ticketsPorEstado
                    .Where(x => x.Estado.Contains("Proceso") || x.Estado.Contains("Programado") || x.Estado.Contains("En ruta"))
                    .Sum(x => x.Cantidad);

                var ticketsCerrados = ticketsPorEstado
                    .Where(x => x.Estado.Contains("Cerrado") || x.Estado.Contains("Completado"))
                    .Sum(x => x.Cantidad);

                // Tickets del mes y del día
                var ticketsDelMes = await _context.Tickets
                    .Where(t => t.CreadoEl >= inicioMes)
                    .CountAsync();

                var ticketsDelDia = await _context.Tickets
                    .Where(t => t.CreadoEl >= inicioDia)
                    .CountAsync();

                // Promedio de tiempo de resolución (en horas)
                var ticketsConTiempo = await _context.Tickets
                    .Where(t => t.CreadoEl.HasValue && t.ModificadoEl.HasValue && 
                               (t.Estado == "Cerrado" || t.Estado == "Completado"))
                    .Select(t => new { t.CreadoEl, t.ModificadoEl })
                    .Take(1000)
                    .ToListAsync();

                var promedioTiempoResolucion = ticketsConTiempo.Any()
                    ? ticketsConTiempo.Average(t => (t.ModificadoEl!.Value - t.CreadoEl!.Value).TotalHours)
                    : 0;

                // Tickets por tipo de servicio
                var ticketsPorTipoServicio = await _context.Tickets
                    .Where(t => t.TipoServicio != null)
                    .GroupBy(t => t.TipoServicio)
                    .Select(g => new TipoServicioStats
                    {
                        Tipo = g.Key!,
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(5)
                    .ToListAsync();

                // Tickets por departamento (con nombres)
                var ticketsPorDepartamento = await (
                    from ticket in _context.Tickets
                    join departamento in _context.Departamentos
                    on ticket.Departamento equals departamento.Id into deptoGroup
                    from departamento in deptoGroup.DefaultIfEmpty()
                    where departamento != null
                    group ticket by departamento.NombreDepartamento into g
                    select new DepartamentoStats
                    {
                        Departamento = g.Key ?? "Sin departamento",
                        Cantidad = g.Count()
                    }
                ).OrderByDescending(x => x.Cantidad)
                .Take(10)
                .ToListAsync();

                // Tickets por empresa (con razón social)
                var ticketsPorEmpresa = await (
                    from ticket in _context.Tickets
                    join cas in _context.CasEmpresas
                    on ticket.Empresa equals cas.IdCas into casGroup
                    from cas in casGroup.DefaultIfEmpty()
                    where cas != null
                    group ticket by cas.RazonSocial into g
                    select new EmpresaStats
                    {
                        Empresa = g.Key ?? "Sin empresa",
                        Cantidad = g.Count()
                    }
                ).OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToListAsync();

                // Tickets por técnico (con nombre completo)
                var ticketsPorTecnico = await (
                    from ticket in _context.Tickets
                    join empleado in _context.Empleados
                    on ticket.Tecnico equals empleado.IdEmpleado into empleadoGroup
                    from empleado in empleadoGroup.DefaultIfEmpty()
                    where empleado != null && ticket.Tecnico != null
                    group ticket by new { empleado.Nombre, empleado.Apellido } into g
                    select new TecnicoStats
                    {
                        Tecnico = (g.Key.Nombre ?? "") + " " + (g.Key.Apellido ?? ""),
                        Cantidad = g.Count()
                    }
                ).OrderByDescending(x => x.Cantidad)
                .Take(10)
                .ToListAsync();

                // Tendencia semanal (últimos 7 días)
                var tendenciaData = await _context.Tickets
                    .Where(t => t.CreadoEl >= hace7Dias && t.CreadoEl.HasValue)
                    .Select(t => t.CreadoEl!.Value.Date)
                    .ToListAsync();

                var tendenciaSemanal = tendenciaData
                    .GroupBy(fecha => fecha)
                    .Select(g => new TendenciaStats
                    {
                        Dia = g.Key.ToString("dd/MM"),
                        Cantidad = g.Count()
                    })
                    .OrderBy(x => x.Dia)
                    .ToList();

                // Completar los 7 días si faltan
                var diasCompletos = new List<TendenciaStats>();
                for (int i = 6; i >= 0; i--)
                {
                    var fecha = now.AddDays(-i).Date;
                    var diaStr = fecha.ToString("dd/MM");
                    var existente = tendenciaSemanal.FirstOrDefault(t => t.Dia == diaStr);
                    diasCompletos.Add(existente ?? new TendenciaStats { Dia = diaStr, Cantidad = 0 });
                }

                // Últimos 5 tickets creados
                var ultimosTickets = await (
                    from ticket in _context.Tickets
                    join cliente in _context.Clientes
                    on ticket.Cliente equals cliente.ID into clienteGroup
                    from cliente in clienteGroup.DefaultIfEmpty()
                    orderby ticket.CreadoEl descending
                    select new TicketResumen
                    {
                        ID = ticket.ID,
                        Estado = ticket.Estado,
                        ClienteNombre = cliente != null 
                            ? (cliente.Nombre ?? "") + " " + (cliente.Apellido ?? "")
                            : null,
                        TipoServicio = ticket.TipoServicio,
                        FechaVisita = ticket.FechaVisita,
                        CreadoEl = ticket.CreadoEl
                    }
                ).Take(5).ToListAsync();

                var stats = new DashboardStats
                {
                    TotalTickets = totalTickets,
                    TicketsPendientes = ticketsPendientes,
                    TicketsEnProceso = ticketsEnProceso,
                    TicketsCerrados = ticketsCerrados,
                    TicketsDelMes = ticketsDelMes,
                    TicketsDelDia = ticketsDelDia,
                    PromedioTiempoResolucion = Math.Round(promedioTiempoResolucion, 1),
                    TicketsPorEstado = ticketsPorEstado,
                    TicketsPorTipoServicio = ticketsPorTipoServicio,
                    TicketsPorDepartamento = ticketsPorDepartamento,
                    TicketsPorEmpresa = ticketsPorEmpresa,
                    TicketsPorTecnico = ticketsPorTecnico,
                    TendenciaSemanal = diasCompletos,
                    UltimosTickets = ultimosTickets
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estadísticas del dashboard");
                return StatusCode(500, "Error al obtener estadísticas del dashboard");
            }
        }
    }
}
