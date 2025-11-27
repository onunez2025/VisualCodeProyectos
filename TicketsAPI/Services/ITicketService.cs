using TicketsAPI.Models;

namespace TicketsAPI.Services
{
    public interface ITicketService
    {
        Task<PagedResult<TicketWithClientDTO>> GetTicketsAsync(TicketQueryParameters parameters);
        Task<TicketWithClientDTO?> GetTicketByIdAsync(string id);
    }
}
