namespace TicketsAPI.Models
{
    public class ClienteQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Nombre { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Departamento { get; set; }
        public string? Distrito { get; set; }
    }
}
