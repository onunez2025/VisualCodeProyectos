using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_PROVINCIA")]
    public class Provincia
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; } = string.Empty;

        [Column("Departamento")]
        public string? Departamento { get; set; }

        [Column("Pais")]
        public string? Pais { get; set; }

        [Column("Provincia")]
        public string? NombreProvincia { get; set; }
    }
}
