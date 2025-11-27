using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;
using TicketsAPI.Data;
using TicketsAPI.Models;

namespace TicketsAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TicketService> _logger;
        private readonly IConfiguration _configuration;

        public TicketService(ApplicationDbContext context, ILogger<TicketService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<PagedResult<TicketWithClientDTO>> GetTicketsAsync(TicketQueryParameters parameters)
        {
            try
            {
                // Query con LEFT JOIN a clientes, productos registrados y materiales
                var query = from ticket in _context.Tickets.AsNoTracking()
                           join cliente in _context.Clientes.AsNoTracking()
                           on ticket.Cliente equals cliente.ID into clienteGroup
                           from cliente in clienteGroup.DefaultIfEmpty()
                           join productoReg in _context.ProductosRegistrados.AsNoTracking()
                           on ticket.ProductosRegistrados equals productoReg.Id into productoGroup
                           from productoReg in productoGroup.DefaultIfEmpty()
                           join material in _context.Materiales.AsNoTracking()
                           on productoReg.Producto equals material.IdMaterial into materialGroup
                           from material in materialGroup.DefaultIfEmpty()
                           join empleado in _context.Empleados.AsNoTracking()
                           on ticket.Tecnico equals empleado.IdEmpleado into empleadoGroup
                           from empleado in empleadoGroup.DefaultIfEmpty()
                           join cas in _context.CasEmpresas.AsNoTracking()
                           on ticket.Empresa equals cas.IdCas into casGroup
                           from cas in casGroup.DefaultIfEmpty()
                           join informeTecnico in _context.InformesTecnicos.AsNoTracking()
                           on ticket.ID equals informeTecnico.Ticket into informeGroup
                           from informeTecnico in informeGroup.DefaultIfEmpty()
                           join departamento in _context.Departamentos.AsNoTracking()
                           on ticket.Departamento equals departamento.Id into deptoGroup
                           from departamento in deptoGroup.DefaultIfEmpty()
                           join provincia in _context.Provincias.AsNoTracking()
                           on ticket.Provincia equals provincia.Id into provGroup
                           from provincia in provGroup.DefaultIfEmpty()
                           join distrito in _context.Distritos.AsNoTracking()
                           on ticket.Distrito equals distrito.Id into distGroup
                           from distrito in distGroup.DefaultIfEmpty()
                           select new TicketWithClientDTO
                           {
                               // Datos del Ticket
                               ID = ticket.ID,
                               Estado = ticket.Estado,
                               Empresa = ticket.Empresa,
                               ClienteID = ticket.Cliente,
                               Celular = ticket.Celular,
                               Celular2 = ticket.Celular2,
                               TelefonoFijo = ticket.TelefonoFijo,
                               CorreoPromotorVenta = ticket.CorreoPromotorVenta,
                               FechaVisita = ticket.FechaVisita,
                               InicioSolicitado = ticket.InicioSolicitado,
                               FinSolicitado = ticket.FinSolicitado,
                               ProductosRegistrados = ticket.ProductosRegistrados,
                               PaisRegion = ticket.PaisRegion,
                               Departamento = ticket.Departamento,
                               Provincia = ticket.Provincia,
                               Distrito = ticket.Distrito,
                               NombreVia = ticket.NombreVia,
                               Numero = ticket.Numero,
                               Zona = ticket.Zona,
                               NombreZona = ticket.NombreZona,
                               Referencia = ticket.Referencia,
                               Descripcion = ticket.Descripcion,
                               TipoServicio = ticket.TipoServicio,
                               MotivoIncidente = ticket.MotivoIncidente,
                               Tecnico = ticket.Tecnico,
                               Ayudante = ticket.Ayudante,
                               CreadoEl = ticket.CreadoEl,
                               CreadoPor = ticket.CreadoPor,
                               ModificadoEl = ticket.ModificadoEl,
                               ModificadoPor = ticket.ModificadoPor,
                               CheckIn = ticket.CheckIn,
                               CheckInLocalizacion = ticket.CheckInLocalizacion,
                               NotasInternas = ticket.NotasInternas,
                               IT = ticket.IT,
                               ImageUrl = ticket.IT != null ? $"/api/files/{Path.GetFileName(ticket.IT)}" : null,
                               
                               // Datos del Cliente
                               ClienteNombre = cliente != null ? cliente.Nombre : null,
                               ClienteApellido = cliente != null ? cliente.Apellido : null,
                               ClienteNombreCompleto = cliente != null 
                                   ? (cliente.Nombre ?? "") + " " + (cliente.Apellido ?? "") 
                                   : null,
                               ClienteLabel = cliente != null 
                                   ? "[" + (cliente.NumeroDocumento ?? "") + "] " + (cliente.Nombre ?? "") + " " + (cliente.Apellido ?? "")
                                   : null,
                               ClienteCelular = cliente != null ? cliente.Celular : null,
                               ClienteCelular2 = cliente != null ? cliente.Celular2 : null,
                               ClienteTelefonoFijo = cliente != null ? cliente.TelefonoFijo : null,
                               ClienteCorreo = cliente != null ? cliente.CorreoElectronico : null,
                               ClienteDepartamento = cliente != null ? cliente.Departamento : null,
                               ClienteProvincia = cliente != null ? cliente.Provincia : null,
                               ClienteDistrito = cliente != null ? cliente.Distrito : null,
                               ClienteDireccion = cliente != null 
                                   ? (cliente.NombreVia ?? "") + " " + (cliente.Numero ?? "") 
                                   : null,
                               ClienteReferencia = cliente != null ? cliente.Referencia : null,
                               ClienteTipoDocumento = cliente != null ? cliente.TipoDocumento : null,
                               ClienteNumeroDocumento = cliente != null ? cliente.NumeroDocumento : null,

                               // Datos del Material
                               MaterialLabel = material != null 
                                   ? "[" + (material.IdExterno ?? "") + "] " + (material.Nombre ?? "")
                                   : null,
                               
                               // Datos del Técnico
                               TecnicoLabel = empleado != null
                                   ? (empleado.Nombre ?? "") + " " + (empleado.Apellido ?? "")
                                   : null,
                               TecnicoNombre = empleado != null ? empleado.Nombre : null,
                               TecnicoApellido = empleado != null ? empleado.Apellido : null,
                               
                               // Datos de la Empresa
                               EmpresaLabel = cas != null ? cas.RazonSocial : null,
                               EmpresaRazonSocial = cas != null ? cas.RazonSocial : null,
                               EmpresaRuc = cas != null ? cas.Ruc : null,
                               
                               // Informe Técnico
                               TieneInformeTecnico = informeTecnico != null,
                               
                               // Departamento
                               DepartamentoNombre = departamento != null ? departamento.NombreDepartamento : null,
                               
                               // Provincia
                               ProvinciaNombre = provincia != null ? provincia.NombreProvincia : null,
                               
                               // Distrito
                               DistritoNombre = distrito != null ? distrito.NombreDistrito : null
                           };

                // Aplicar filtros
                if (!string.IsNullOrWhiteSpace(parameters.NumeroTicket))
                {
                    query = query.Where(t => t.ID != null && 
                        t.ID.Contains(parameters.NumeroTicket));
                }

                if (!string.IsNullOrWhiteSpace(parameters.NombreCliente))
                {
                    query = query.Where(t => 
                        (t.ClienteNombreCompleto != null && t.ClienteNombreCompleto.Contains(parameters.NombreCliente)) ||
                        (t.ClienteNombre != null && t.ClienteNombre.Contains(parameters.NombreCliente)) ||
                        (t.ClienteApellido != null && t.ClienteApellido.Contains(parameters.NombreCliente)) ||
                        (t.ClienteNumeroDocumento != null && t.ClienteNumeroDocumento.Contains(parameters.NombreCliente)));
                }

                if (!string.IsNullOrWhiteSpace(parameters.NombreProducto))
                {
                    query = query.Where(t => t.ProductosRegistrados != null && 
                        t.ProductosRegistrados.Contains(parameters.NombreProducto));
                }

                if (parameters.FechaVisitaDesde.HasValue)
                {
                    query = query.Where(t => t.FechaVisita >= parameters.FechaVisitaDesde.Value);
                }

                if (parameters.FechaVisitaHasta.HasValue)
                {
                    query = query.Where(t => t.FechaVisita <= parameters.FechaVisitaHasta.Value);
                }

                // Obtener total de registros
                var totalRecords = await query.CountAsync();

                // Aplicar paginación y ordenar por fecha de creación descendente
                var data = await query
                    .OrderByDescending(t => t.CreadoEl)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

                return new PagedResult<TicketWithClientDTO>
                {
                    Data = data,
                    TotalRecords = totalRecords,
                    PageNumber = parameters.PageNumber,
                    PageSize = parameters.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener tickets");
                throw;
            }
        }

        public async Task<TicketWithClientDTO?> GetTicketByIdAsync(string id)
        {
            try
            {
                var result = await (from ticket in _context.Tickets.AsNoTracking()
                                   join cliente in _context.Clientes.AsNoTracking()
                                   on ticket.Cliente equals cliente.ID into clienteGroup
                                   from cliente in clienteGroup.DefaultIfEmpty()
                                   join productoReg in _context.ProductosRegistrados.AsNoTracking()
                                   on ticket.ProductosRegistrados equals productoReg.Id into productoGroup
                                   from productoReg in productoGroup.DefaultIfEmpty()
                                   join material in _context.Materiales.AsNoTracking()
                                   on productoReg.Producto equals material.IdMaterial into materialGroup
                                   from material in materialGroup.DefaultIfEmpty()
                                   join empleado in _context.Empleados.AsNoTracking()
                                   on ticket.Tecnico equals empleado.IdEmpleado into empleadoGroup
                                   from empleado in empleadoGroup.DefaultIfEmpty()
                                   join cas in _context.CasEmpresas.AsNoTracking()
                                   on ticket.Empresa equals cas.IdCas into casGroup
                                   from cas in casGroup.DefaultIfEmpty()
                                   join informeTecnico in _context.InformesTecnicos.AsNoTracking()
                                   on ticket.ID equals informeTecnico.Ticket into informeGroup
                                   from informeTecnico in informeGroup.DefaultIfEmpty()
                                   join departamento in _context.Departamentos.AsNoTracking()
                                   on ticket.Departamento equals departamento.Id into deptoGroup
                                   from departamento in deptoGroup.DefaultIfEmpty()
                                   join provincia in _context.Provincias.AsNoTracking()
                                   on ticket.Provincia equals provincia.Id into provGroup
                                   from provincia in provGroup.DefaultIfEmpty()
                                   join distrito in _context.Distritos.AsNoTracking()
                                   on ticket.Distrito equals distrito.Id into distGroup
                                   from distrito in distGroup.DefaultIfEmpty()
                                   where ticket.ID == id
                                   select new TicketWithClientDTO
                                   {
                                       // Datos del Ticket
                                       ID = ticket.ID,
                                       Estado = ticket.Estado,
                                       Empresa = ticket.Empresa,
                                       ClienteID = ticket.Cliente,
                                       Celular = ticket.Celular,
                                       Celular2 = ticket.Celular2,
                                       TelefonoFijo = ticket.TelefonoFijo,
                                       CorreoPromotorVenta = ticket.CorreoPromotorVenta,
                                       FechaVisita = ticket.FechaVisita,
                                       InicioSolicitado = ticket.InicioSolicitado,
                                       FinSolicitado = ticket.FinSolicitado,
                                       ProductosRegistrados = ticket.ProductosRegistrados,
                                       PaisRegion = ticket.PaisRegion,
                                       Departamento = ticket.Departamento,
                                       Provincia = ticket.Provincia,
                                       Distrito = ticket.Distrito,
                                       NombreVia = ticket.NombreVia,
                                       Numero = ticket.Numero,
                                       Zona = ticket.Zona,
                                       NombreZona = ticket.NombreZona,
                                       Referencia = ticket.Referencia,
                                       Descripcion = ticket.Descripcion,
                                       TipoServicio = ticket.TipoServicio,
                                       MotivoIncidente = ticket.MotivoIncidente,
                                       Tecnico = ticket.Tecnico,
                                       Ayudante = ticket.Ayudante,
                                       CreadoEl = ticket.CreadoEl,
                                       CreadoPor = ticket.CreadoPor,
                                       ModificadoEl = ticket.ModificadoEl,
                                       ModificadoPor = ticket.ModificadoPor,
                                       CheckIn = ticket.CheckIn,
                                       CheckInLocalizacion = ticket.CheckInLocalizacion,
                                       NotasInternas = ticket.NotasInternas,
                                       IT = ticket.IT,
                                       
                                       // Datos del Cliente
                                       ClienteNombre = cliente != null ? cliente.Nombre : null,
                                       ClienteApellido = cliente != null ? cliente.Apellido : null,
                                       ClienteNombreCompleto = cliente != null 
                                           ? (cliente.Nombre ?? "") + " " + (cliente.Apellido ?? "") 
                                           : null,
                                       ClienteLabel = cliente != null 
                                           ? "[" + (cliente.NumeroDocumento ?? "") + "] " + (cliente.Nombre ?? "") + " " + (cliente.Apellido ?? "")
                                           : null,
                                       ClienteCelular = cliente != null ? cliente.Celular : null,
                                       ClienteCelular2 = cliente != null ? cliente.Celular2 : null,
                                       ClienteTelefonoFijo = cliente != null ? cliente.TelefonoFijo : null,
                                       ClienteCorreo = cliente != null ? cliente.CorreoElectronico : null,
                                       ClienteDepartamento = cliente != null ? cliente.Departamento : null,
                                       ClienteProvincia = cliente != null ? cliente.Provincia : null,
                                       ClienteDistrito = cliente != null ? cliente.Distrito : null,
                                       ClienteDireccion = cliente != null 
                                           ? (cliente.NombreVia ?? "") + " " + (cliente.Numero ?? "") 
                                           : null,
                                       ClienteReferencia = cliente != null ? cliente.Referencia : null,
                                       ClienteTipoDocumento = cliente != null ? cliente.TipoDocumento : null,
                                       ClienteNumeroDocumento = cliente != null ? cliente.NumeroDocumento : null,

                                       // Datos del Material
                                       MaterialLabel = material != null 
                                           ? "[" + (material.IdExterno ?? "") + "] " + (material.Nombre ?? "")
                                           : null,
                                       
                                       // Datos del Técnico
                                       TecnicoLabel = empleado != null
                                           ? (empleado.Nombre ?? "") + " " + (empleado.Apellido ?? "")
                                           : null,
                                       TecnicoNombre = empleado != null ? empleado.Nombre : null,
                                       TecnicoApellido = empleado != null ? empleado.Apellido : null,
                                       
                                       // Datos de la Empresa
                                       EmpresaLabel = cas != null ? cas.RazonSocial : null,
                                       EmpresaRazonSocial = cas != null ? cas.RazonSocial : null,
                                       EmpresaRuc = cas != null ? cas.Ruc : null,
                                       
                                       // Informe Técnico
                                       TieneInformeTecnico = informeTecnico != null,
                                       
                                       // Departamento
                                       DepartamentoNombre = departamento != null ? departamento.NombreDepartamento : null,
                                       
                                       // Provincia
                                       ProvinciaNombre = provincia != null ? provincia.NombreProvincia : null,
                                       
                                       // Distrito
                                       DistritoNombre = distrito != null ? distrito.NombreDistrito : null
                                   }).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener ticket con ID {id}");
                throw;
            }
        }
    }
}
