namespace TicketsAPI.Models
{
    public class TicketWithClientDTO
    {
        // Datos del Ticket
        public string ID { get; set; } = string.Empty;
        public string? Estado { get; set; }
        public string? Empresa { get; set; }
        public string? ClienteID { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? TelefonoFijo { get; set; }
        public string? CorreoPromotorVenta { get; set; }
        public DateTime? FechaVisita { get; set; }
        public DateTime? InicioSolicitado { get; set; }
        public DateTime? FinSolicitado { get; set; }
        public string? ProductosRegistrados { get; set; }
        public string? PaisRegion { get; set; }
        public string? Departamento { get; set; }
        public string? Provincia { get; set; }
        public string? Distrito { get; set; }
        public string? NombreVia { get; set; }
        public string? Numero { get; set; }
        public string? Zona { get; set; }
        public string? NombreZona { get; set; }
        public string? Referencia { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoServicio { get; set; }
        public string? MotivoIncidente { get; set; }
        public string? Tecnico { get; set; }
        public string? Ayudante { get; set; }
        public DateTime? CreadoEl { get; set; }
        public string? CreadoPor { get; set; }
        public DateTime? ModificadoEl { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? CheckIn { get; set; }
        public string? CheckInLocalizacion { get; set; }
        public string? NotasInternas { get; set; }
        public string? IT { get; set; }
        public string? ImageUrl { get; set; } // URL pública del archivo en OneDrive

        // Datos del Cliente (desde GACP_APP_TB_CLIENTES)
        public string? ClienteNombre { get; set; }
        public string? ClienteApellido { get; set; }
        public string? ClienteNombreCompleto { get; set; }
        public string? ClienteLabel { get; set; } // [DNI] Nombre Apellido
        public string? ClienteCelular { get; set; }
        public string? ClienteCelular2 { get; set; }
        public string? ClienteTelefonoFijo { get; set; }
        public string? ClienteCorreo { get; set; }
        public string? ClienteDepartamento { get; set; }
        public string? ClienteProvincia { get; set; }
        public string? ClienteDistrito { get; set; }
        public string? ClienteDireccion { get; set; }
        public string? ClienteReferencia { get; set; }
        public string? ClienteTipoDocumento { get; set; }
        public string? ClienteNumeroDocumento { get; set; }

        // Datos del Material (desde GAC_APP_TB_MATERIALES via PRODUCTO_REGISTRADO)
        public string? MaterialLabel { get; set; } // [ID_Externo] Nombre
        
        // Datos del Técnico (desde GACP_APP_TB_EMPLEADOS)
        public string? TecnicoLabel { get; set; } // Nombre Apellido
        public string? TecnicoNombre { get; set; }
        public string? TecnicoApellido { get; set; }
        
        // Datos de la Empresa (desde GACP_APP_TB_CAS)
        public string? EmpresaLabel { get; set; }
        public string? EmpresaRazonSocial { get; set; }
        public string? EmpresaRuc { get; set; }
        
        // Informe Técnico
        public bool TieneInformeTecnico { get; set; }
        
        // Departamento
        public string? DepartamentoNombre { get; set; }
        
        // Provincia
        public string? ProvinciaNombre { get; set; }
        
        // Distrito
        public string? DistritoNombre { get; set; }
    }
}
