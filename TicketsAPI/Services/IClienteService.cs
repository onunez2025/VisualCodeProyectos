using TicketsAPI.Models;

namespace TicketsAPI.Services
{
    public interface IClienteService
    {
        Task<PagedResult<Cliente>> GetClientesAsync(ClienteQueryParameters parameters);
        Task<Cliente?> GetClienteByIdAsync(string id);
    }
}
