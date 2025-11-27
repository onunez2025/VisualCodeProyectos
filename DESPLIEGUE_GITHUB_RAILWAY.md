# 🚀 Guía de Despliegue: GitHub Pages + Railway

## 📋 Resumen

Esta guía te ayudará a publicar tu Sistema de Tickets en:
- **Frontend (Angular)**: GitHub Pages → `https://onunez2025.github.io/VisualCodeProyectos`
- **Backend (.NET)**: Railway → se generará automáticamente

---

## ✅ Prerequisitos

- [x] Cuenta de GitHub: `onunez2025`
- [ ] Git instalado en Windows
- [ ] Node.js y npm instalados
- [ ] Crear cuenta en Railway.app

---

## 🔥 PARTE 1: Subir el Proyecto a GitHub

### Paso 1.1: Verificar Git

Abre PowerShell y ejecuta:

```powershell
git --version
```

**Si Git NO está instalado:**
1. Descarga desde: https://git-scm.com/download/win
2. Instala con las opciones por defecto
3. Reinicia PowerShell

### Paso 1.2: Configurar Git (primera vez)

```powershell
git config --global user.name "onunez2025"
git config --global user.email "tu-email@gmail.com"
```

> ⚠️ **Reemplaza** `tu-email@gmail.com` con tu email de GitHub

### Paso 1.3: Inicializar Repositorio

En PowerShell, navega a tu proyecto:

```powershell
cd "c:\Users\onunez\OneDrive - MT INDUSTRIAL S.A.C\Escritorio\VisualCodeProyectos"
```

Inicializa Git:

```powershell
git init
git add .
git commit -m "Initial commit - Sistema de Tickets"
```

### Paso 1.4: Crear Repositorio en GitHub

1. Ve a: https://github.com/new
2. **Repository name**: `VisualCodeProyectos` (o el nombre que prefieras)
3. **Visibility**: Escoge **Public** o **Private** (si quieres que sea privado)
4. **NO marques** "Add a README file"
5. Click en **"Create repository"**

### Paso 1.5: Conectar con GitHub

GitHub te mostrará comandos. Usa estos (ajusta el nombre del repo si es diferente):

```powershell
git remote add origin https://github.com/onunez2025/VisualCodeProyectos.git
git branch -M main
git push -u origin main
```

**Te pedirá credenciales:**
- **Usuario**: `onunez2025`
- **Contraseña**: Usa un **Personal Access Token** (GitHub ya no acepta contraseñas)

#### Crear Personal Access Token (si es necesario):
1. Ve a: https://github.com/settings/tokens
2. Click en **"Generate new token (classic)"**
3. Marca el scope: **`repo`**
4. Click **"Generate token"**
5. **Copia el token** (solo se muestra una vez)
6. Úsalo como contraseña cuando hagas `git push`

---

## 🚂 PARTE 2: Desplegar Backend en Railway

### Paso 2.1: Crear Cuenta en Railway

1. Ve a: https://railway.app
2. Click en **"Start a New Project"** o **"Login with GitHub"**
3. Autoriza Railway para acceder a tu GitHub

### Paso 2.2: Crear Nuevo Proyecto

1. En Railway Dashboard, click **"New Project"**
2. Selecciona **"Deploy from GitHub repo"**
3. Busca y selecciona: **`VisualCodeProyectos`**
4. Railway detectará el Dockerfile automáticamente

### Paso 2.3: Configurar Variables de Entorno

En Railway, ve a tu proyecto → **Variables**:

Agrega esta variable:

```
ConnectionStrings__DefaultConnection=Server=soledbserver.database.windows.net;Database=soledb-puntoventa;User Id=sole_readuser;Password=uXu34wCx6brPq#PJ;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

> 📌 **Nota:** Usa dos guiones bajos `__` entre `ConnectionStrings` y `DefaultConnection`

**Opcional - Variables de OneDrive** (solo si tienes integración cloud):
```
OneDrive__LocalSyncPath=ruta-cloud
OneDrive__InformeTecnicoImagesPath=ruta-cloud
```

### Paso 2.4: Desplegar

1. Railway empezará a construir automáticamente
2. Espera 2-5 minutos mientras se despliega
3. Una vez completado, verás un mensaje de **"Success"**

### Paso 2.5: Obtener URL del Backend

1. En tu proyecto de Railway, click en **"Settings"**
2. Busca la sección **"Domains"**
3. Click en **"Generate Domain"**
4. Railway te dará una URL como: `https://tu-proyecto.up.railway.app`

**🎯 GUARDA ESTA URL** - la necesitarás para el frontend

**Ejemplo de URL:**
```
https://visualcodeproyectos-production.up.railway.app
```

### Paso 2.6: Verificar que Funciona

Abre en tu navegador:
```
https://tu-proyecto.up.railway.app
```

Deberías ver la interfaz de **Swagger** con la documentación de tu API.

Prueba el endpoint:
```
https://tu-proyecto.up.railway.app/api/tickets
```

---

## 🌐 PARTE 3: Configurar y Desplegar Frontend en GitHub Pages

### Paso 3.1: Actualizar URL del Backend

Abre el archivo:
```
Plantilla_Angular/src/environments/environment.prod.ts
```

Cambia `apiUrl` con la URL de Railway que obtuviste:

```typescript
import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  apiUrl: 'https://TU-PROYECTO.up.railway.app/api'  // ⬅️ CAMBIA ESTO
};
```

### Paso 3.2: Configurar Base Href para GitHub Pages

Edita `angular.json` en la sección `projects` → `architect` → `build` → `configurations` → `production`:

Agrega esta línea dentro de `"production"`:

```json
"baseHref": "/VisualCodeProyectos/",
```

**Ejemplo completo:**
```json
"production": {
  "baseHref": "/VisualCodeProyectos/",
  "budgets": [...],
  "fileReplacements": [...]
}
```

> ⚠️ Si tu repositorio tiene otro nombre, usa `/nombre-del-repo/`

### Paso 3.3: Build de Producción

En PowerShell, desde la carpeta del proyecto:

```powershell
cd Plantilla_Angular
npm install
npm run build -- --configuration=production
```

Esto creará la carpeta `dist/` con los archivos listos para publicar.

### Paso 3.4: Instalar Angular CLI GitHub Pages

```powershell
npm install -g angular-cli-ghpages
```

### Paso 3.5: Publicar en GitHub Pages

Desde `Plantilla_Angular/`:

```powershell
npx angular-cli-ghpages --dir=dist --no-silent
```

**Si te pide credenciales**, usa:
- Usuario: `onunez2025`
- Token: El Personal Access Token que creaste antes

### Paso 3.6: Activar GitHub Pages (si es necesario)

1. Ve a tu repositorio en GitHub
2. Click en **Settings** → **Pages**
3. En **Source**, selecciona: **`gh-pages`** branch
4. Click **Save**

---

## ✅ PARTE 4: Actualizar CORS del Backend

Ahora que ya conoces la URL exacta de GitHub Pages, actualiza el backend:

### Paso 4.1: Editar Program.cs

Abre:
```
TicketsAPI/Program.cs
```

Ya está configurado para `https://onunez2025.github.io`, pero verifica que la línea esté presente:

```csharp
.WithOrigins(
    "http://localhost:4200",
    "https://siat-provincias.azurewebsites.net",
    "https://onunez2025.github.io"  // ✅ Esta línea
)
```

### Paso 4.2: Subir Cambios a GitHub

```powershell
cd ..
git add .
git commit -m "Update: Railway deployment configuration"
git push origin main
```

Railway detectará los cambios y re-desplegará automáticamente (1-2 minutos).

---

## 🎉 PARTE 5: Verificación Final

### URLs de tu Aplicación:

- **Frontend**: `https://onunez2025.github.io/VisualCodeProyectos`
- **Backend**: `https://tu-proyecto.up.railway.app`
- **Swagger**: `https://tu-proyecto.up.railway.app` (raíz)

### Pruebas:

1. **Abre el Frontend** en tu navegador
2. Navega a la sección **Tickets**
3. Verifica que:
   - ✅ Los tickets cargan correctamente
   - ✅ Los filtros funcionan
   - ✅ La paginación funciona
   - ✅ No hay errores de CORS en la consola (F12)

### Abrir Consola del Navegador:

1. Presiona **F12** en tu navegador
2. Ve a la pestaña **Console**
3. NO deberías ver errores rojos

---

## 🔧 Troubleshooting

### ❌ Error: "CORS policy blocked"

**Solución:**
1. Verifica que la URL en `environment.prod.ts` sea correcta
2. Verifica que `Program.cs` tenga tu dominio de GitHub Pages
3. Re-despliega el backend en Railway

### ❌ Error: "404 Not Found" al refrescar la página

**Solución:**
Crea archivo `404.html` en `Plantilla_Angular/src/`:

```html
<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="refresh" content="0;URL='/VisualCodeProyectos/'">
</head>
<body></body>
</html>
```

Luego re-build y re-publica.

### ❌ Backend no conecta con SQL Server

**Solución:**
1. Ve a Railway → tu proyecto → **Variables**
2. Verifica que `ConnectionStrings__DefaultConnection` esté correcta
3. Asegúrate que SQL Server Azure permita conexiones externas
4. Ve a Azure Portal → SQL Server → **Firewalls** → Agrega regla para permitir Azure services

### ❌ Error al hacer `git push`

**Solución:**
- Usa Personal Access Token en lugar de contraseña
- Verifica que el token tenga permisos de `repo`

---

## 🔄 Actualizar el Proyecto en el Futuro

### Actualizar Backend:

```powershell
cd TicketsAPI
# Hacer cambios en el código
cd ..
git add .
git commit -m "Descripción del cambio"
git push origin main
```

Railway re-desplegará automáticamente.

### Actualizar Frontend:

```powershell
cd Plantilla_Angular
# Hacer cambios en el código
npm run build -- --configuration=production
npx angular-cli-ghpages --dir=dist --no-silent
```

---

## 📊 Monitoreo

### Railway Dashboard:
- **Logs**: Ver logs en tiempo real
- **Metrics**: CPU, RAM, Network
- **Deployments**: Historial de despliegues

### GitHub:
- **Actions**: Ver historial de builds
- **Settings → Pages**: Estado del sitio

---

## 💰 Costos

- **GitHub Pages**: ✅ Gratis (ilimitado para repositorios públicos)
- **Railway**: ✅ $5 de crédito gratis mensual (suficiente para proyectos pequeños)
  - Si se acaba, puedes agregar tarjeta de crédito
  - Costo estimado: $0-5/mes dependiendo del uso

---

## 📞 Soporte

Si tienes problemas:
1. Revisa los logs en Railway
2. Abre la consola del navegador (F12)
3. Verifica que todas las URLs estén correctas

**Links útiles:**
- Railway Docs: https://docs.railway.app
- GitHub Pages: https://pages.github.com
- Angular CLI: https://angular.io/cli

---

¡Felicidades! 🎉 Tu aplicación está en línea y disponible para todo el mundo.
