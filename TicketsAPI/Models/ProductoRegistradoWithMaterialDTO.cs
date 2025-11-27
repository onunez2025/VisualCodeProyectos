namespace TicketsAPI.Models
{
    public class ProductoRegistradoWithMaterialDTO
    {
        // Datos del Producto Registrado
        public string Id { get; set; } = string.Empty;
        public string? Cliente { get; set; }
        public string? Producto { get; set; }
        public string? CategoriaProductoRegistrado { get; set; }
        public string? Departamento { get; set; }
        public string? Provincia { get; set; }
        public string? Distrito { get; set; }
        public string? EdificioPlantaSala { get; set; }
        public string? NombreVia { get; set; }
        public string? Numero { get; set; }
        public string? LugarCompra { get; set; }
        public string? Zona { get; set; }
        public string? NombreZona { get; set; }
        public string? Referencia { get; set; }
        public string? Garantia { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public DateTime? CreadoEl { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public string? ModificadoPor { get; set; }

        // Datos del Material
        public string? MaterialLabel { get; set; } // [ID_Externo] Nombre
        public string? MaterialNombre { get; set; }
        public string? MaterialCategoria { get; set; }
        public string? MaterialIdExterno { get; set; }
    }
}
