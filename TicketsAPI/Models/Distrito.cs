using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_DISTRITO")]
    public class Distrito
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; } = string.Empty;

        [Column("Provincia")]
        public string? Provincia { get; set; }

        [Column("Departamento")]
        public string? Departamento { get; set; }

        [Column("Pais")]
        public string? Pais { get; set; }

        [Column("Distrito")]
        public string? NombreDistrito { get; set; }
    }
}
