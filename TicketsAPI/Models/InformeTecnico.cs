using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_INFORME_TECNICO")]
    public class InformeTecnico
    {
        [Key]
        [Column("ID_informe_tecnico")]
        public string IdInformeTecnico { get; set; } = string.Empty;

        [Column("Ticket")]
        public string? Ticket { get; set; }

        [Column("Visita_realizada")]
        public string? VisitaRealizada { get; set; }

        [Column("Trabajo_efectuado")]
        public string? TrabajoEfectuado { get; set; }

        [Column("Observaciones_tecnico")]
        public string? ObservacionesTecnico { get; set; }

        [Column("Serie_producto")]
        public string? SerieProducto { get; set; }

        [Column("Estado")]
        public string? Estado { get; set; }
        
        [Column("Firma_tecnico")]
        public string? FirmaTecnico { get; set; }

        [Column("Firma_cliente")]
        public string? FirmaCliente { get; set; }

        [Column("Creado_el")]
        public DateTime? CreadoEl { get; set; }

        [Column("Creado_por")]
        public string? CreadoPor { get; set; }

        [Column("Modificado_el")]
        public DateTime? ModificadoEl { get; set; }

        [Column("Modificado_por")]
        public string? ModificadoPor { get; set; }
    }
}
