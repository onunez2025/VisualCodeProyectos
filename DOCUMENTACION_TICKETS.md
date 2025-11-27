# 🎯 Sistema de Gestión de Tickets - Documentación

## 📋 Resumen del Proyecto

Sistema completo con backend .NET 8 Web API y frontend Angular 20 para la visualización de tickets desde SQL Server Azure.

---

## 🏗️ Arquitectura

### Backend - .NET 8 Web API
- **Puerto:** http://localhost:5270
- **Swagger:** http://localhost:5270/swagger
- **Base de datos:** SQL Server Azure (soledb-puntoventa)
- **Modo:** Solo lectura

### Frontend - Angular 20
- **Puerto:** http://localhost:4200
- **Plantilla:** Datta Able Admin Template
- **Framework UI:** Bootstrap 5

---

## 📁 Estructura del Proyecto

```
TicketsAPI/                          # Backend .NET
├── Controllers/
│   └── TicketsController.cs         # Endpoints API
├── Data/
│   └── ApplicationDbContext.cs      # Contexto EF Core
├── Models/
│   ├── Ticket.cs                    # Modelo de datos
│   └── TicketQueryParameters.cs     # Paginación y filtros
├── Services/
│   ├── ITicketService.cs            # Interfaz del servicio
│   └── TicketService.cs             # Lógica de negocio
├── appsettings.json                 # Configuración (conexión DB)
└── Program.cs                       # Configuración de la app

Plantilla_Angular/                   # Frontend Angular
├── src/app/
│   ├── models/
│   │   └── ticket.model.ts          # Interfaces TypeScript
│   ├── services/
│   │   └── ticket.service.ts        # Servicio HTTP
│   ├── demo/pages/tickets/
│   │   ├── tickets.component.ts     # Componente principal
│   │   ├── tickets.component.html   # Template
│   │   └── tickets.component.scss   # Estilos
│   └── theme/layout/admin/navigation/
│       └── navigation.ts            # Menú de navegación
└── src/main.ts                      # Configuración HttpClient
```

---

## 🔌 API Endpoints

### GET /api/tickets
Obtiene lista paginada de tickets con filtros opcionales.

**Parámetros de consulta:**
```
pageNumber     : number  (default: 1)
pageSize       : number  (default: 20)
numeroTicket   : string  (opcional)
nombreCliente  : string  (opcional)
nombreProducto : string  (opcional)
fechaVisitaDesde : date  (opcional)
fechaVisitaHasta : date  (opcional)
```

**Respuesta:**
```json
{
  "data": [
    {
      "ticketID": 1,
      "numeroTicket": "TKT-001",
      "nombreCliente": "Cliente Demo",
      "nombreProducto": "Producto X",
      "fechaVisita": "2025-11-20T00:00:00",
      "estado": "Pendiente",
      "prioridad": "Alta",
      "descripcion": "...",
      "tecnicoAsignado": "Juan Pérez",
      ...
    }
  ],
  "totalRecords": 150,
  "pageNumber": 1,
  "pageSize": 20,
  "totalPages": 8,
  "hasPreviousPage": false,
  "hasNextPage": true
}
```

### GET /api/tickets/{id}
Obtiene un ticket específico por ID.

---

## 🎨 Características del Frontend

### ✅ Funcionalidades Implementadas

1. **Tabla de Tickets**
   - Diseño responsivo con Bootstrap 5
   - Visualización de datos en formato tabla
   - Badges de colores para estado y prioridad
   - Iconos Feather integrados

2. **Paginación**
   - 20 registros por página
   - Navegación con botones anterior/siguiente
   - Selector de páginas numeradas
   - Información de registros mostrados

3. **Filtros de Búsqueda**
   - Filtro por número de ticket
   - Filtro por cliente
   - Filtro por producto
   - Filtro por rango de fechas de visita
   - Botones para aplicar y limpiar filtros

4. **Estados Visuales**
   - Indicador de carga (spinner)
   - Mensajes de error
   - Mensaje cuando no hay resultados
   - Hover effects en filas de tabla

5. **Badges de Estado**
   - **Pendiente:** Amarillo (warning)
   - **En Proceso:** Azul (info)
   - **Completado:** Verde (success)
   - **Cancelado:** Rojo (danger)

6. **Badges de Prioridad**
   - **Alta:** Rojo (danger)
   - **Media:** Amarillo (warning)
   - **Baja:** Verde (success)

---

## 🚀 Cómo Ejecutar el Proyecto

### 1. Iniciar el Backend (.NET)

```powershell
cd TicketsAPI
dotnet run
```

El servidor estará disponible en: **http://localhost:5270**

### 2. Iniciar el Frontend (Angular)

```powershell
cd Plantilla_Angular
npm start
```

La aplicación estará disponible en: **http://localhost:4200**

### 3. Acceder a la Vista de Tickets

- Abre tu navegador en **http://localhost:4200**
- En el menú lateral, haz clic en **"Tickets"**
- O navega directamente a **http://localhost:4200/tickets**

---

## 🔐 Credenciales de Base de Datos

**Tipo:** Solo lectura (Read-only)

```
Servidor: soledbserver.database.windows.net
Base de datos: soledb-puntoventa
Usuario: sole_readuser
Contraseña: uXu34wCx6brPq#PJ
```

**Tabla consultada:** `[dbo].[GACP_APP_TB_TICKETS]`

---

## 📊 Modelo de Datos

### Campos de la Tabla Tickets

```typescript
{
  ticketID: number;              // ID único del ticket
  numeroTicket: string;          // Número de ticket (ej: TKT-001)
  clienteID: number;             // ID del cliente
  nombreCliente: string;         // Nombre del cliente
  productoID: number;            // ID del producto
  nombreProducto: string;        // Nombre del producto
  fechaVisita: Date;             // Fecha programada de visita
  estado: string;                // Estado del ticket
  prioridad: string;             // Prioridad (Alta, Media, Baja)
  descripcion: string;           // Descripción del ticket
  tecnicoAsignado: string;       // Técnico responsable
  fechaCreacion: Date;           // Fecha de creación
  fechaActualizacion: Date;      // Última actualización
}
```

---

## 🛠️ Tecnologías Utilizadas

### Backend
- **.NET 8.0** - Framework principal
- **Entity Framework Core 8.0.11** - ORM
- **Dapper 2.1.66** - Micro-ORM para consultas optimizadas
- **SQL Server** - Base de datos

### Frontend
- **Angular 20.0.5** - Framework principal
- **Bootstrap 5.3.7** - Framework CSS
- **Feather Icons** - Librería de iconos
- **RxJS** - Programación reactiva
- **TypeScript 5.8.3** - Lenguaje

---

## 📝 Próximas Mejoras Sugeridas

### Funcionalidades Adicionales
1. ✅ Vista de detalles del ticket (modal o página separada)
2. ✅ Exportar a Excel/PDF
3. ✅ Gráficos y estadísticas (ApexCharts ya incluido)
4. ✅ Búsqueda avanzada con múltiples criterios
5. ✅ Ordenamiento por columnas
6. ✅ Filtros guardados/favoritos
7. ✅ Notificaciones en tiempo real (SignalR)
8. ✅ Dashboard con KPIs de tickets

### Mejoras Técnicas
1. ✅ Implementar caché en el backend
2. ✅ Agregar logs estructurados (Serilog)
3. ✅ Tests unitarios y de integración
4. ✅ Autenticación y autorización (JWT)
5. ✅ Rate limiting en API
6. ✅ Versionamiento de API

---

## 🔍 Testing

### Probar la API directamente

**Con Swagger:**
```
http://localhost:5270/swagger
```

**Con curl:**
```bash
# Obtener tickets con paginación
curl "http://localhost:5270/api/tickets?pageNumber=1&pageSize=20"

# Filtrar por cliente
curl "http://localhost:5270/api/tickets?nombreCliente=MTIndustrial"

# Obtener ticket específico
curl "http://localhost:5270/api/tickets/1"
```

---

## 🐛 Solución de Problemas

### Error de CORS
Si aparece error de CORS, verifica que:
1. El backend esté corriendo en `http://localhost:5270`
2. La URL en `ticket.service.ts` sea correcta
3. CORS esté habilitado en `Program.cs`

### Error de conexión a SQL Server
Si hay problemas de conexión:
1. Verifica las credenciales en `appsettings.json`
2. Comprueba que tu IP esté en la whitelist de Azure
3. Revisa la cadena de conexión

### Página en blanco en Angular
Si la página no carga:
1. Verifica la consola del navegador (F12)
2. Asegúrate de que el backend esté corriendo
3. Revisa que HttpClient esté configurado en `main.ts`

---

## 📞 Soporte

Para más información sobre la plantilla Datta Able:
- **Documentación:** https://codedthemes.gitbook.io/datta-angular/
- **Demo:** https://codedthemes.com/demos/admin-templates/datta-able/angular/free/dashboard

---

## 📄 Licencia

- **Backend:** Código propietario
- **Frontend Template:** MIT License (Datta Able Free)

---

## ✅ Checklist de Implementación

- [x] Backend .NET Web API creado
- [x] Conexión a SQL Server Azure configurada
- [x] Modelo de datos Ticket implementado
- [x] Servicio de tickets con paginación
- [x] API con filtros implementada
- [x] CORS habilitado para Angular
- [x] Frontend Angular configurado
- [x] HttpClient provider agregado
- [x] Servicio Angular para API
- [x] Componente de tickets creado
- [x] Tabla con diseño de plantilla
- [x] Paginación funcional (20 por página)
- [x] Filtros por ticket, cliente, producto, fecha
- [x] Ruta agregada al routing
- [x] Menú de navegación actualizado
- [x] Estilos personalizados aplicados
- [x] Badges de estado y prioridad

---

¡Sistema listo para usar! 🎉
