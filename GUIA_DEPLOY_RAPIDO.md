# 🚀 Guía de Deploy Rápido - Sistema de Tickets

## ✅ Opción 1: Railway (RECOMENDADO - Más Fácil que Azure)

### 🎯 **Ventajas de Railway**
- ✅ **Gratis** (500 horas/mes)
- ✅ **Despliegue automático** desde GitHub
- ✅ **Base de datos incluida** (PostgreSQL gratis)
- ✅ **SSL automático**
- ✅ **No requiere tarjeta de crédito**

### 📋 **Paso 1: Preparar el Repositorio**

```powershell
# 1. Hacer commit de los cambios
git add .
git commit -m "feat: preparar para deploy en Railway"
git push origin main
```

### 🚂 **Paso 2: Deploy del Backend (.NET API)**

1. **Ir a Railway**: https://railway.app
2. **Login con GitHub**
3. **New Project** → **Deploy from GitHub repo**
4. **Seleccionar**: `VisualCodeProyectos`
5. **Root Directory**: `/TicketsAPI`
6. **Configurar Variables de Entorno**:
   - Click en el servicio → Variables
   - Agregar:
     ```
     ASPNETCORE_ENVIRONMENT=Production
     ASPNETCORE_URLS=http://0.0.0.0:$PORT
     ```

7. **Agregar Connection String**:
   - En Variables, agregar:
     ```
     ConnectionStrings__DefaultConnection=Server=soledbserver.database.windows.net;Database=soledb-puntoventa;User Id=sole_readuser;Password=uXu34wCx6brPq#PJ;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
     ```

8. **Deploy** - Railway automáticamente detectará .NET y compilará

9. **Obtener URL** - Copiar la URL generada (ej: `https://tu-app.railway.app`)

### 🎨 **Paso 3: Deploy del Frontend (Angular)**

**Opción A: Vercel (Recomendado para Angular)**

1. **Ir a Vercel**: https://vercel.com
2. **Login con GitHub**
3. **Import Project** → Seleccionar `VisualCodeProyectos`
4. **Configure**:
   - Framework Preset: **Angular**
   - Root Directory: `Plantilla_Angular`
   - Build Command: `npm run vercel-build`
   - Output Directory: `dist`

5. **Environment Variables**:
   - Click en "Environment Variables"
   - Agregar:
     ```
     RAILWAY_API_URL=https://tu-app.railway.app/api
     ```

6. **Deploy** - Click en "Deploy"

7. **Actualizar API URL**:
   - Ir a `src/environments/environment.prod.ts`
   - Cambiar `apiUrl` a tu URL de Railway

**Opción B: Railway (Todo en un lugar)**

1. **En Railway**, crear otro servicio
2. **Deploy from GitHub** → Mismo repo
3. **Root Directory**: `/Plantilla_Angular`
4. **Settings** → **Custom Build Command**:
   ```
   npm install && npm run build
   ```
5. **Custom Start Command**:
   ```
   npx http-server dist -p $PORT
   ```

---

## ✅ Opción 2: Render (Alternativa Gratuita)

### Backend (.NET)
1. **Ir a**: https://render.com
2. **New** → **Web Service**
3. **Connect GitHub** → Seleccionar repo
4. **Configurar**:
   - Name: `tickets-api`
   - Root Directory: `TicketsAPI`
   - Environment: `Docker`
   - Build Command: `dotnet publish -c Release -o out`
   - Start Command: `dotnet out/TicketsAPI.dll`

### Frontend (Angular)
1. **New** → **Static Site**
2. **Connect GitHub** → Seleccionar repo
3. **Configurar**:
   - Name: `tickets-frontend`
   - Root Directory: `Plantilla_Angular`
   - Build Command: `npm install && npm run build`
   - Publish Directory: `dist`

---

## 🐳 Opción 3: Docker + Fly.io (Gratis)

### Crear Dockerfile para .NET

```dockerfile
# Ya existe en la raíz del proyecto
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TicketsAPI/TicketsAPI.csproj", "TicketsAPI/"]
RUN dotnet restore "TicketsAPI/TicketsAPI.csproj"
COPY . .
WORKDIR "/src/TicketsAPI"
RUN dotnet build "TicketsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketsAPI.dll"]
```

### Deploy con Fly.io

```powershell
# Instalar CLI
powershell -Command "iwr https://fly.io/install.ps1 -useb | iex"

# Login
fly auth login

# Deploy
fly launch --dockerfile Dockerfile
```

---

## 📊 Comparación de Opciones

| Plataforma | Facilidad | Gratis | Build Time | SSL | Recomendado |
|------------|-----------|--------|------------|-----|-------------|
| **Railway** | ⭐⭐⭐⭐⭐ | ✅ 500h/mes | ~3 min | ✅ | ✅ **SÍ** |
| **Vercel (Frontend)** | ⭐⭐⭐⭐⭐ | ✅ Ilimitado | ~2 min | ✅ | ✅ **SÍ** |
| **Render** | ⭐⭐⭐⭐ | ✅ 750h/mes | ~5 min | ✅ | ⭐ Bueno |
| **Fly.io** | ⭐⭐⭐ | ✅ Limitado | ~4 min | ✅ | ⭐ OK |
| **Azure** | ⭐⭐ | ❌ Requiere $ | ~8 min | ✅ | ❌ Complejo |

---

## 🎯 MI RECOMENDACIÓN

### **Setup Ideal (100% Gratis)**

1. **Backend** → **Railway** (más fácil para .NET)
2. **Frontend** → **Vercel** (especializado en Angular/React)

### **Ventajas**:
- ✅ Deploy en **5 minutos**
- ✅ **Totalmente gratis**
- ✅ **SSL automático**
- ✅ **CI/CD** incluido (auto-deploy en push)
- ✅ **Logs en tiempo real**
- ✅ **Más fácil que Azure**

---

## 🔧 Actualizar CORS después del Deploy

Una vez tengas las URLs, actualizar en `TicketsAPI/Program.cs`:

```csharp
var allowedOrigins = new[] {
    "http://localhost:4200",                    // Local
    "https://tu-app.vercel.app",               // Vercel Frontend
    "https://onunez2025.github.io"             // GitHub Pages (si usas)
};
```

Y hacer push:
```powershell
git add .
git commit -m "fix: actualizar CORS con URLs de producción"
git push
```

Railway y Vercel **auto-deployarán** automáticamente.

---

## 📞 ¿Necesitas Ayuda?

Si tienes problemas:
1. **Railway**: Ver logs en tiempo real en el dashboard
2. **Vercel**: Click en el deployment → Ver build logs
3. **Errores comunes**: Ver sección de troubleshooting abajo

---

## 🐛 Troubleshooting

### Error: "Application failed to start"
- Verificar que `ASPNETCORE_URLS` esté configurado
- Revisar logs en Railway dashboard

### Error: "Connection refused"
- Verificar Connection String en variables de entorno
- Verificar que SQL Server permita conexiones externas

### Frontend no carga
- Verificar que `apiUrl` en `environment.prod.ts` apunte a Railway
- Verificar CORS en backend

---

## ✅ Checklist de Deploy

- [ ] Código en GitHub actualizado
- [ ] Backend desplegado en Railway
- [ ] Frontend desplegado en Vercel
- [ ] Variables de entorno configuradas
- [ ] CORS actualizado con URLs de producción
- [ ] Probar endpoints de API
- [ ] Probar frontend completo
- [ ] SSL funcionando (https)

---

**¿Listo para empezar? Te ayudo paso a paso si necesitas.**
