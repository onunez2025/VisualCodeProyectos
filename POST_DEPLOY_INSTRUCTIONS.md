# Instrucciones Post-Deploy

## ✅ Después de desplegar en Railway/Vercel

### 1. Obtener URL del Backend (Railway)
Una vez desplegado en Railway, obtendrás una URL como:
```
https://tu-proyecto.railway.app
```

### 2. Actualizar environment.prod.ts

Editar: `Plantilla_Angular/src/environments/environment.prod.ts`

```typescript
export const environment = {
  appVersion: packageInfo.version,
  production: true,
  apiUrl: 'https://tu-proyecto.railway.app/api'  // ← Cambiar aquí
};
```

### 3. Actualizar CORS en Backend

Editar: `TicketsAPI/Program.cs` (línea ~18)

```csharp
var allowedOrigins = new[] {
    "http://localhost:4200",                // Local
    "https://tu-frontend.vercel.app",      // ← Tu URL de Vercel
    "https://onunez2025.github.io"         // GitHub Pages (opcional)
};
```

### 4. Hacer Commit y Push

```powershell
git add .
git commit -m "fix: actualizar URLs de producción"
git push origin main
```

Railway y Vercel **auto-deployarán** automáticamente ✅

### 5. Verificar

- [ ] Backend funcionando: `https://tu-proyecto.railway.app/health`
- [ ] Swagger accesible: `https://tu-proyecto.railway.app/swagger`
- [ ] Frontend carga: `https://tu-frontend.vercel.app`
- [ ] API se conecta desde frontend
- [ ] No hay errores CORS en consola

## 🎯 URLs a Configurar

Después del deploy, tendrás 2 URLs:

1. **Backend (Railway)**: `https://__________.railway.app`
2. **Frontend (Vercel)**: `https://__________.vercel.app`

Actualiza:
- ✅ `environment.prod.ts` con URL del backend
- ✅ `Program.cs` con URL del frontend
- ✅ Hacer push para redeploy automático
