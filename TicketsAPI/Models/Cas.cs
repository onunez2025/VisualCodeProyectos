using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_CAS")]
    public class Cas
    {
        [Key]
        [Column("ID_cas")]
        public string IdCas { get; set; } = string.Empty;

        [Column("Ruc")]
        public string? Ruc { get; set; }

        [Column("Razon_social")]
        public string? RazonSocial { get; set; }

        [Column("Pais_Region")]
        public string? PaisRegion { get; set; }

        [Column("Departamento")]
        public string? Departamento { get; set; }

        [Column("Provincia")]
        public string? Provincia { get; set; }

        [Column("Distrito")]
        public string? Distrito { get; set; }

        [Column("Celular")]
        public string? Celular { get; set; }

        [Column("Celular_2")]
        public string? Celular2 { get; set; }

        [Column("Telefono_fijo")]
        public string? TelefonoFijo { get; set; }

        [Column("Correo_electronico")]
        public string? CorreoElectronico { get; set; }

        [Column("Logo")]
        public string? Logo { get; set; }

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
