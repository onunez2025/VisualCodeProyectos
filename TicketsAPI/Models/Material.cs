using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GAC_APP_TB_MATERIALES")]
    public class Material
    {
        [Key]
        [Column("ID_Material")]
        public string IdMaterial { get; set; } = string.Empty;

        [Column("ID_Externo")]
        public string? IdExterno { get; set; }

        [Column("Nombre")]
        public string? Nombre { get; set; }

        [Column("Categoria")]
        public string? Categoria { get; set; }

        [Column("Unidad_medida")]
        public string? UnidadMedida { get; set; }

        [Column("Uso")]
        public string? Uso { get; set; }

        [Column("Estado")]
        public string? Estado { get; set; }

        [Column("Garantia")]
        public string? Garantia { get; set; }

        [Column("Sector")]
        public string? Sector { get; set; }

        [Column("Descuento")]
        public string? Descuento { get; set; }

        [Column("EstadoEnCatalogo")]
        public string? EstadoEnCatalogo { get; set; }
    }
}
