using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_TICKETS", Schema = "dbo")]
    public class Ticket
    {
        [Key]
        [Column("ID")]
        public string ID { get; set; } = string.Empty;

        [Column("Estado")]
        public string? Estado { get; set; }

        [Column("Empresa")]
        public string? Empresa { get; set; }

        [Column("Cliente")]
        public string? Cliente { get; set; }

        [Column("Celular")]
        public string? Celular { get; set; }

        [Column("Celula_2")]
        public string? Celular2 { get; set; }

        [Column("Telefono_fijo")]
        public string? TelefonoFijo { get; set; }

        [Column("Correo_promotor_venta")]
        public string? CorreoPromotorVenta { get; set; }

        [Column("Fecha_visita")]
        public DateTime? FechaVisita { get; set; }

        [Column("Inicio_solicitado")]
        public DateTime? InicioSolicitado { get; set; }

        [Column("Fin_solicitado")]
        public DateTime? FinSolicitado { get; set; }

        [Column("Productos registrados")]
        public string? ProductosRegistrados { get; set; }

        [Column("Pais_region")]
        public string? PaisRegion { get; set; }

        [Column("Departamento")]
        public string? Departamento { get; set; }

        [Column("Provincia")]
        public string? Provincia { get; set; }

        [Column("Distrito")]
        public string? Distrito { get; set; }

        [Column("Nombre_via")]
        public string? NombreVia { get; set; }

        [Column("Numero")]
        public string? Numero { get; set; }

        [Column("Zona")]
        public string? Zona { get; set; }

        [Column("Nombre_zona")]
        public string? NombreZona { get; set; }

        [Column("Referencia")]
        public string? Referencia { get; set; }

        [Column("Descripcion")]
        public string? Descripcion { get; set; }

        [Column("Tipo_servicio")]
        public string? TipoServicio { get; set; }

        [Column("Motivo_incidente")]
        public string? MotivoIncidente { get; set; }

        [Column("Tecnico")]
        public string? Tecnico { get; set; }

        [Column("Ayudante")]
        public string? Ayudante { get; set; }

        [Column("Creado_el")]
        public DateTime? CreadoEl { get; set; }

        [Column("Creado_por")]
        public string? CreadoPor { get; set; }

        [Column("Modificado_el")]
        public DateTime? ModificadoEl { get; set; }

        [Column("Modificado_por")]
        public string? ModificadoPor { get; set; }

        [Column("Check_in")]
        public DateTime? CheckIn { get; set; }

        [Column("Check_in_localizacion")]
        public string? CheckInLocalizacion { get; set; }

        [Column("Notas_internas")]
        public string? NotasInternas { get; set; }

        [Column("IT")]
        public string? IT { get; set; }
    }
}
