export interface Ticket {
  id: string;
  estado: string | null;
  empresa: string | null;
  clienteID: string | null;
  celular: string | null;
  celular2: string | null;
  telefonoFijo: string | null;
  correoPromotorVenta: string | null;
  fechaVisita: string | null;
  inicioSolicitado: string | null;
  finSolicitado: string | null;
  productosRegistrados: string | null;
  paisRegion: string | null;
  departamento: string | null;
  provincia: string | null;
  distrito: string | null;
  nombreVia: string | null;
  numero: string | null;
  zona: string | null;
  nombreZona: string | null;
  referencia: string | null;
  descripcion: string | null;
  tipoServicio: string | null;
  motivoIncidente: string | null;
  tecnico: string | null;
  ayudante: string | null;
  creadoEl: string | null;
  creadoPor: string | null;
  modificadoEl: string | null;
  modificadoPor: string | null;
  checkIn: string | null;
  checkInLocalizacion: string | null;
  notasInternas: string | null;
  it: string | null;
  imageUrl: string | null; // URL pública del archivo en OneDrive
  
  // Datos del Cliente desde GACP_APP_TB_CLIENTES
  clienteNombre: string | null;
  clienteApellido: string | null;
  clienteNombreCompleto: string | null;
  clienteLabel: string | null; // [DNI] Nombre Apellido
  clienteCelular: string | null;
  clienteCelular2: string | null;
  clienteTelefonoFijo: string | null;
  clienteCorreo: string | null;
  clienteDepartamento: string | null;
  clienteProvincia: string | null;
  clienteDistrito: string | null;
  clienteDireccion: string | null;
  clienteReferencia: string | null;
  clienteTipoDocumento: string | null;
  clienteNumeroDocumento: string | null;

  // Datos del Material desde GAC_APP_TB_MATERIALES
  materialLabel: string | null; // [ID_Externo] Nombre
  
  // Datos del Técnico desde GACP_APP_TB_EMPLEADOS
  tecnicoLabel: string | null; // Nombre Apellido
  tecnicoNombre: string | null;
  tecnicoApellido: string | null;
  
  // Datos de la Empresa desde GACP_APP_TB_CAS
  empresaLabel: string | null; // Razon_social
  empresaRazonSocial: string | null;
  empresaRuc: string | null;
  
  // Informe Técnico
  tieneInformeTecnico?: boolean;
  
  // Departamento
  departamentoNombre: string | null;
  
  // Provincia
  provinciaNombre: string | null;
  
  // Distrito
  distritoNombre: string | null;
}

export interface TicketQueryParameters {
  pageNumber?: number;
  pageSize?: number;
  numeroTicket?: string;
  nombreCliente?: string;
  nombreProducto?: string;
  fechaVisitaDesde?: string;
  fechaVisitaHasta?: string;
}

export interface PagedResult<T> {
  data: T[];
  totalRecords: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
