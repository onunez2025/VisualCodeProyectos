namespace TicketsAPI.Models
{
    public class TicketQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        
        // Filtros
        public string? NumeroTicket { get; set; }
        public string? NombreCliente { get; set; }
        public string? NombreProducto { get; set; }
        public DateTime? FechaVisitaDesde { get; set; }
        public DateTime? FechaVisitaHasta { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
