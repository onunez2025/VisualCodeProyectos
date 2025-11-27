using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_EMPLEADOS")]
    public class Empleado
    {
        [Key]
        [Column("ID_empleado")]
        public string IdEmpleado { get; set; } = string.Empty;

        [Column("Nombre")]
        public string? Nombre { get; set; }

        [Column("Apellido")]
        public string? Apellido { get; set; }

        [Column("Tipo_documento")]
        public string? TipoDocumento { get; set; }

        [Column("Numero_documento")]
        public string? NumeroDocumento { get; set; }

        [Column("Celular")]
        public string? Celular { get; set; }

        [Column("Correo_electronico")]
        public string? CorreoElectronico { get; set; }

        [Column("Cargo")]
        public string? Cargo { get; set; }

        [Column("Area")]
        public string? Area { get; set; }

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
