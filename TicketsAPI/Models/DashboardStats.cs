namespace TicketsAPI.Models
{
    public class DashboardStats
    {
        public int TotalTickets { get; set; }
        public int TicketsPendientes { get; set; }
        public int TicketsEnProceso { get; set; }
        public int TicketsCerrados { get; set; }
        public int TicketsDelMes { get; set; }
        public int TicketsDelDia { get; set; }
        public double PromedioTiempoResolucion { get; set; }
        public List<EstadoStats> TicketsPorEstado { get; set; } = new();
        public List<TipoServicioStats> TicketsPorTipoServicio { get; set; } = new();
        public List<DepartamentoStats> TicketsPorDepartamento { get; set; } = new();
        public List<EmpresaStats> TicketsPorEmpresa { get; set; } = new();
        public List<TecnicoStats> TicketsPorTecnico { get; set; } = new();
        public List<TendenciaStats> TendenciaSemanal { get; set; } = new();
        public List<TicketResumen> UltimosTickets { get; set; } = new();
    }

    public class EstadoStats
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class TipoServicioStats
    {
        public string Tipo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class DepartamentoStats
    {
        public string Departamento { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class EmpresaStats
    {
        public string Empresa { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class TecnicoStats
    {
        public string Tecnico { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class TendenciaStats
    {
        public string Dia { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class TicketResumen
    {
        public string ID { get; set; } = string.Empty;
        public string? Estado { get; set; }
        public string? ClienteNombre { get; set; }
        public string? TipoServicio { get; set; }
        public DateTime? FechaVisita { get; set; }
        public DateTime? CreadoEl { get; set; }
    }
}
