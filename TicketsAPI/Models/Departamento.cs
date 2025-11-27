using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_DEPARTAMENTO")]
    public class Departamento
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; } = string.Empty;

        [Column("Pais")]
        public string? Pais { get; set; }

        [Column("Departamento")]
        public string? NombreDepartamento { get; set; }
    }
}
