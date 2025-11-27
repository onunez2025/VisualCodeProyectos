# Implementación de Imágenes desde OneDrive

## Resumen de la Implementación

Se ha configurado el sistema para mostrar imágenes almacenadas en OneDrive directamente en la plataforma web.

## Rutas Configuradas

### Backend (appsettings.json)
```json
"OneDrive": {
  "LocalSyncPath": "C:\\Users\\onunez\\OneDrive - MT INDUSTRIAL S.A.C (1)",
  "InformeTecnicoImagesPath": "C:\\Users\\onunez\\OneDrive - MT INDUSTRIAL S.A.C (1)\\appsheet\\data\\SIATP-266763730\\INFORME_TECNICO_Images",
  "InformeTecnicoFilesPath": "C:\\Users\\onunez\\OneDrive - MT INDUSTRIAL S.A.C (1)\\appsheet\\data\\SIATP-266763730\\INFORME_TECNICO_Files_",
  "FilesPath": "C:\\Users\\onunez\\OneDrive - MT INDUSTRIAL S.A.C (1)\\appsheet\\data\\SIATP-266763730\\appsheet\\data\\SIATP-266763730\\Files"
}
```

### Ubicaciones de Archivos
- **PDFs de Tickets**: `C:\Users\onunez\OneDrive - MT INDUSTRIAL S.A.C (1)\appsheet\data\SIATP-266763730\appsheet\data\SIATP-266763730\Files`
- **Imágenes del Informe Técnico**: `C:\Users\onunez\OneDrive - MT INDUSTRIAL S.A.C (1)\appsheet\data\SIATP-266763730\INFORME_TECNICO_Images`
- **Archivos del Informe Técnico**: `C:\Users\onunez\OneDrive - MT INDUSTRIAL S.A.C (1)\appsheet\data\SIATP-266763730\INFORME_TECNICO_Files_`

## Nuevos Endpoints API

### 1. GET /api/files/{fileName}
Sirve archivos desde las carpetas configuradas de OneDrive.

**Ejemplo:**
```
GET http://localhost:5270/api/files/OS_EC48ECB0.pdf
```

Busca el archivo en este orden:
1. `Files/{fileName}` - PDFs de tickets (OS_*.pdf)
2. `INFORME_TECNICO_Files_/{fileName}` - Archivos del informe técnico
3. `INFORME_TECNICO_Images/{fileName}` - Imágenes del informe técnico

### 2. GET /api/files/informe/{fileName}
Endpoint específico para imágenes del informe técnico.

**Ejemplo:**
```
GET http://localhost:5270/api/files/informe/0001ACB7.Firma_cliente.012734.png
```

Sirve archivos desde `INFORME_TECNICO_Images`

### 3. GET /api/files/{fileName}/preview
Vista previa del archivo (actualmente retorna el archivo completo).

## Modelo de Datos Actualizado

### TicketWithClientDTO (Backend)
```csharp
public string? IT { get; set; }  // Ruta original del archivo
public string? ImageUrl { get; set; }  // URL pública del archivo
```

La propiedad `ImageUrl` se genera automáticamente en el servicio:
```csharp
ImageUrl = ticket.IT != null ? $"/api/files/{Path.GetFileName(ticket.IT)}" : null
```

### Ticket Model (Frontend)
```typescript
it: string | null;
imageUrl: string | null;
```

## Visualización en el Frontend

### Componente de Tickets
El componente incluye funciones para manejar diferentes tipos de archivos:

#### Funciones Auxiliares
```typescript
getImageUrl(imageUrl: string): string
isImageFile(fileName: string): boolean
isPdfFile(fileName: string): boolean
getFileName(filePath: string): string
onImageError(event: Event): void
```

#### Extensiones Soportadas
- **Imágenes**: .jpg, .jpeg, .png, .gif, .bmp, .webp
- **PDF**: .pdf
- **Otros**: Muestra icono genérico

### Plantilla HTML
El modal de detalles del ticket incluye una sección de "Archivos Adjuntos" que:

1. **Para imágenes**: Muestra vista previa con `<img>`
2. **Para PDFs**: Muestra icono de PDF
3. **Para otros**: Muestra icono genérico de archivo
4. **Botones**:
   - Abrir en nueva pestaña
   - Descargar archivo

### Imagen Placeholder
Ubicación: `assets/images/placeholder.svg`

Se muestra cuando:
- La imagen falla al cargar
- No se encuentra el archivo
- Error en la URL

## Tipos de Contenido Soportados

El FilesController maneja automáticamente estos tipos:
- `.pdf` → application/pdf
- `.jpg`, `.jpeg` → image/jpeg
- `.png` → image/png
- `.gif` → image/gif
- `.doc` → application/msword
- `.docx` → application/vnd.openxmlformats-officedocument.wordprocessingml.document
- `.xls` → application/vnd.ms-excel
- `.xlsx` → application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
- Otros → application/octet-stream

## Flujo de Datos

### 1. Backend (TicketService)
```
Ticket.IT → Extrae nombre de archivo → Genera ImageUrl (/api/files/{nombre})
```

### 2. API (FilesController)
```
GET /api/files/{nombre} → Busca en carpetas configuradas → Retorna archivo
```

### 3. Frontend (TicketsComponent)
```
ImageUrl → getImageUrl() → Construye URL completa → <img src="...">
```

## Seguridad y Consideraciones

### Ventajas
✅ Archivos servidos desde backend con validación
✅ No expone rutas locales al cliente
✅ Control de tipos de archivo
✅ Manejo de errores con placeholder
✅ Registro de intentos de acceso (logs)

### Requisitos
⚠️ OneDrive debe estar sincronizado localmente
⚠️ El servidor backend debe tener acceso a la carpeta de OneDrive
⚠️ Permisos de lectura en las carpetas configuradas

## Ejemplo de Uso

### Ver imagen en el modal de detalle
1. Usuario abre un ticket
2. Si tiene archivo adjunto (campo `IT`), se muestra la sección "Archivos Adjuntos"
3. La imagen se carga automáticamente desde `/api/files/{nombre}`
4. Si falla, se muestra el placeholder SVG

### Probar endpoint manualmente
```powershell
Invoke-RestMethod -Uri "http://localhost:5270/api/files/informe/0001ACB7.Firma_cliente.012734.png" -OutFile "imagen.png"
```

## Archivos Modificados

### Backend
1. `TicketsAPI/Services/OneDriveService.cs` - Servicio para convertir rutas
2. `TicketsAPI/Controllers/FilesController.cs` - Endpoints para servir archivos
3. `TicketsAPI/Models/TicketWithClientDTO.cs` - Agregado campo `ImageUrl`
4. `TicketsAPI/Services/TicketService.cs` - Genera URLs automáticamente
5. `TicketsAPI/Program.cs` - Registra `IOneDriveService`
6. `TicketsAPI/appsettings.json` - Configuración de rutas OneDrive

### Frontend
1. `src/app/models/ticket.model.ts` - Agregado campo `imageUrl`
2. `src/app/demo/pages/tickets/tickets.component.ts` - Funciones auxiliares
3. `src/app/demo/pages/tickets/tickets.component.html` - Sección de archivos adjuntos
4. `src/assets/images/placeholder.svg` - Imagen placeholder

## Próximos Pasos (Opcional)

### Posibles Mejoras
1. **Caché de imágenes**: Implementar caché en el backend para mejorar rendimiento
2. **Miniaturas**: Generar thumbnails automáticos para vista previa
3. **Galería**: Crear galería de imágenes para tickets con múltiples archivos
4. **Subida de archivos**: Permitir adjuntar nuevos archivos desde la interfaz
5. **Microsoft Graph API**: Usar Graph API para acceso directo a OneDrive sin sincronización local

### Alternativa con Microsoft Graph API
Si en el futuro se requiere acceso sin sincronización local:
1. Configurar autenticación OAuth 2.0
2. Obtener token de acceso
3. Usar Microsoft Graph API para obtener URLs de descarga directa
4. Ventaja: No requiere OneDrive sincronizado
5. Desventaja: Requiere configuración de Azure AD
