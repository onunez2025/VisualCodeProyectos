export interface ProductoRegistrado {
  id: string;
  cliente: string | null;
  producto: string | null;
  categoriaProductoRegistrado: string | null;
  departamento: string | null;
  provincia: string | null;
  distrito: string | null;
  edificioPlantaSala: string | null;
  nombreVia: string | null;
  numero: string | null;
  lugarCompra: string | null;
  zona: string | null;
  nombreZona: string | null;
  referencia: string | null;
  garantia: string | null;
  latitud: string | null;
  longitud: string | null;
  creadoEl: string | null;
  creadoPor: string | null;
  modificadoEl: string | null;
  modificadoPor: string | null;
  
  // Datos del Material
  materialLabel: string | null; // [ID_Externo] Nombre
  materialNombre: string | null;
  materialCategoria: string | null;
  materialIdExterno: string | null;
}
