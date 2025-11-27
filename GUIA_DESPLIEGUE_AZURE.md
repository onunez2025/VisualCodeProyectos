# GUÍA DE DESPLIEGUE EN AZURE

## Configuración Completada

### URLs Configuradas
- **Producción**: https://siat-provincias.azurewebsites.net
- **Desarrollo**: http://localhost:4200 (frontend) | http://localhost:5270 (backend)

### Cambios Realizados

#### Frontend (Angular)
1. ✅ Configurado `environment.ts` para desarrollo (localhost)
2. ✅ Configurado `environment.prod.ts` para producción (Azure)
3. ✅ Todos los servicios usan `environment.apiUrl`
4. ✅ URLs dinámicas para imágenes

#### Backend (.NET)
1. ✅ CORS configurado para localhost y Azure
2. ✅ `web.config` creado para IIS/Azure App Service
3. ✅ Headers de seguridad configurados

---

## Pasos para Desplegar

### 1. Build de Producción - Frontend

```powershell
cd Plantilla_Angular
npm run build -- --configuration=production
```

Esto generará la carpeta `dist/` con los archivos optimizados.

### 2. Build de Producción - Backend

```powershell
cd TicketsAPI
dotnet publish -c Release -o ./publish
```

### 3. Desplegar en Azure

#### Opción A: Azure Portal (Manual)
1. Ir a https://portal.azure.com
2. Crear un **App Service** (o usar existente)
3. Subir contenido de `TicketsAPI/publish` al App Service
4. Subir contenido de `Plantilla_Angular/dist/datta-able-free-angular-admin-template` a Azure Storage (Static Website)

#### Opción B: Azure CLI

**Backend:**
```powershell
az webapp up --name siat-provincias --resource-group <tu-resource-group> --runtime "DOTNETCORE:8.0"
```

**Frontend (Static Web App):**
```powershell
az staticwebapp create --name siat-provincias-frontend --resource-group <tu-resource-group> --source Plantilla_Angular --location "eastus"
```

### 4. Configurar Connection String en Azure

En Azure Portal → App Service → Configuration → Connection strings:
```
Name: DefaultConnection
Value: <tu-connection-string-sql-server>
Type: SQLServer
```

### 5. Configurar Variables de Entorno (Opcional)

En Azure Portal → App Service → Configuration → Application settings:
```
ASPNETCORE_ENVIRONMENT=Production
WEBSITE_RUN_FROM_PACKAGE=1
```

---

## Verificaciones Post-Despliegue

### ✅ Checklist
- [ ] Backend responde en `https://siat-provincias.azurewebsites.net/api/health`
- [ ] Frontend carga correctamente
- [ ] CORS funciona (sin errores en consola del navegador)
- [ ] Base de datos SQL Server está accesible
- [ ] Imágenes de OneDrive se cargan correctamente

### Pruebas Recomendadas
1. Abrir `https://siat-provincias.azurewebsites.net`
2. Verificar dashboard
3. Listar tickets
4. Listar clientes
5. Ver detalles de ticket con imágenes

---

## Troubleshooting

### Error: CORS
- Verificar que la URL del frontend está en `Program.cs` → `WithOrigins()`

### Error: 500 Internal Server Error
- Revisar logs en Azure Portal → App Service → Log Stream
- Verificar connection string de la base de datos

### Frontend no conecta con Backend
- Verificar que `environment.prod.ts` tiene la URL correcta
- Rebuild con `ng build --configuration=production`

### Imágenes no cargan
- Verificar que el servicio de OneDrive tiene las credenciales correctas
- Revisar permisos de la aplicación en Azure AD

---

## Comandos Útiles

### Ver logs en Azure
```powershell
az webapp log tail --name siat-provincias --resource-group <resource-group>
```

### Reiniciar App Service
```powershell
az webapp restart --name siat-provincias --resource-group <resource-group>
```

### Actualizar Backend
```powershell
cd TicketsAPI
dotnet publish -c Release -o ./publish
az webapp deployment source config-zip --name siat-provincias --resource-group <resource-group> --src publish.zip
```

---

## Contacto y Soporte
- Fecha de configuración: 25 de noviembre de 2025
- Configurado para: Azure App Service
- Stack: .NET 8.0 + Angular 19
