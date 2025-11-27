using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Models;

namespace TicketsAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(ApplicationDbContext context, ILogger<ClienteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedResult<Cliente>> GetClientesAsync(ClienteQueryParameters parameters)
        {
            try
            {
                var query = _context.Clientes.AsNoTracking().AsQueryable();

                // Aplicar filtros
                if (!string.IsNullOrWhiteSpace(parameters.Nombre))
                {
                    query = query.Where(c => 
                        (c.Nombre != null && c.Nombre.Contains(parameters.Nombre)) ||
                        (c.Apellido != null && c.Apellido.Contains(parameters.Nombre)));
                }

                if (!string.IsNullOrWhiteSpace(parameters.NumeroDocumento))
                {
                    query = query.Where(c => c.NumeroDocumento != null && 
                        c.NumeroDocumento.Contains(parameters.NumeroDocumento));
                }

                if (!string.IsNullOrWhiteSpace(parameters.Departamento))
                {
                    query = query.Where(c => c.Departamento != null && 
                        c.Departamento.Contains(parameters.Departamento));
                }

                if (!string.IsNullOrWhiteSpace(parameters.Distrito))
                {
                    query = query.Where(c => c.Distrito != null && 
                        c.Distrito.Contains(parameters.Distrito));
                }

                // Obtener total de registros
                var totalRecords = await query.CountAsync();

                // Aplicar paginación y ordenar por fecha de creación descendente
                var data = await query
                    .OrderByDescending(c => c.CreadoEl)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

                return new PagedResult<Cliente>
                {
                    Data = data,
                    TotalRecords = totalRecords,
                    PageNumber = parameters.PageNumber,
                    PageSize = parameters.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes");
                throw;
            }
        }

        public async Task<Cliente?> GetClienteByIdAsync(string id)
        {
            try
            {
                return await _context.Clientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.ID == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener cliente con ID {id}");
                throw;
            }
        }
    }
}
