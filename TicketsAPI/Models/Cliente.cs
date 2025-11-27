using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_CLIENTES")]
    public class Cliente
    {
        [Key]
        [Column("ID_cliente")]
        public string? ID { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }

        [Column("Apellido")]
        public string? Apellido { get; set; }

        [Column("Pais_Region")]
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

        [Column("Referencia_adicional")]
        public string? ReferenciaAdicional { get; set; }

        [Column("Nro_int_dpto")]
        public string? NroIntDpto { get; set; }

        [Column("Celular")]
        public string? Celular { get; set; }

        [Column("Celular_2")]
        public string? Celular2 { get; set; }

        [Column("Telefono_fijo")]
        public string? TelefonoFijo { get; set; }

        [Column("Correo_electronico")]
        public string? CorreoElectronico { get; set; }

        [Column("Pais_region_fiscal")]
        public string? PaisRegionFiscal { get; set; }

        [Column("Nacionalidad")]
        public string? Nacionalidad { get; set; }

        [Column("Tipo_documento")]
        public string? TipoDocumento { get; set; }

        [Column("Numero_documento")]
        public string? NumeroDocumento { get; set; }

        [Column("Creado_el")]
        public DateTime? CreadoEl { get; set; }

        [Column("Creado_por")]
        public string? CreadoPor { get; set; }

        [Column("Modificado_el")]
        public DateTime? ModificadoEl { get; set; }

        [Column("Modificado_por")]
        public string? ModificadoPor { get; set; }

        [Column("Moroso")]
        public string? Moroso { get; set; }
    }
}
