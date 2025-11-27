using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsAPI.Models
{
    [Table("GACP_APP_TB_PRODUCTO_REGISTRADO")]
    public class ProductoRegistrado
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; } = string.Empty;

        [Column("Cliente")]
        public string? Cliente { get; set; }

        [Column("Producto")]
        public string? Producto { get; set; }

        [Column("Categoria_producto_registrado")]
        public string? CategoriaProductoRegistrado { get; set; }

        [Column("Pais_region")]
        public string? PaisRegion { get; set; }

        [Column("Departamento")]
        public string? Departamento { get; set; }

        [Column("Provincia")]
        public string? Provincia { get; set; }

        [Column("Distrito")]
        public string? Distrito { get; set; }

        [Column("Edificio_planta_sala")]
        public string? EdificioPlantaSala { get; set; }

        [Column("Nombre_via")]
        public string? NombreVia { get; set; }

        [Column("Numero")]
        public string? Numero { get; set; }

        [Column("Lugar_compra")]
        public string? LugarCompra { get; set; }

        [Column("Zona")]
        public string? Zona { get; set; }

        [Column("Nombre_zona")]
        public string? NombreZona { get; set; }

        [Column("Referencia")]
        public string? Referencia { get; set; }

        [Column("Garantia")]
        public string? Garantia { get; set; }

        [Column("Latitud")]
        public string? Latitud { get; set; }

        [Column("Longitud")]
        public string? Longitud { get; set; }

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
