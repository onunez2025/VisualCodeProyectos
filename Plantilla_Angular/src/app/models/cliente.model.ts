export interface Cliente {
  id: string;
  nombre: string | null;
  apellido: string | null;
  paisRegion: string | null;
  departamento: string | null;
  provincia: string | null;
  distrito: string | null;
  nombreVia: string | null;
  numero: string | null;
  zona: string | null;
  nombreZona: string | null;
  referencia: string | null;
  referenciaAdicional: string | null;
  nroIntDpto: string | null;
  celular: string | null;
  celular2: string | null;
  telefonoFijo: string | null;
  correoElectronico: string | null;
  paisRegionFiscal: string | null;
  nacionalidad: string | null;
  tipoDocumento: string | null;
  numeroDocumento: string | null;
  creadoEl: string | null;
  creadoPor: string | null;
  modificadoEl: string | null;
  modificadoPor: string | null;
  moroso: string | null;
}

export interface ClienteQueryParameters {
  pageNumber?: number;
  pageSize?: number;
  nombre?: string;
  numeroDocumento?: string;
  departamento?: string;
  distrito?: string;
}
