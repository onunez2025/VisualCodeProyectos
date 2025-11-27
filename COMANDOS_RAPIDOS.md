# ⚡ Comandos Rápidos - Post Optimización

## 🔍 Verificar Estado del Proyecto

```powershell
# Ver errores de compilación TypeScript
cd Plantilla_Angular
npx tsc --noEmit

# Ver tamaño del proyecto
Get-ChildItem -Recurse | Measure-Object -Property Length -Sum | Select-Object @{Name="Total(GB)";Expression={[math]::Round($_.Sum/1GB,2)}}
```

## 🧹 Limpieza Rápida (Un Comando)

```powershell
# Ejecutar desde la raíz
.\Limpiar-Archivos.ps1
```

## 🏗️ Build Completo

```powershell
# Frontend
cd Plantilla_Angular
npm run build -- --configuration production

# Backend
cd ..\TicketsAPI
dotnet publish -c Release -o publish
```

## 🧪 Testing

```powershell
# Angular (si hay tests configurados)
cd Plantilla_Angular
npm test

# .NET (si hay tests)
cd TicketsAPI
dotnet test
```

## 📦 Preparar para Deploy

```powershell
# 1. Limpiar
.\Limpiar-Archivos.ps1

# 2. Build Angular
cd Plantilla_Angular
npm run build -- --configuration production

# 3. Build .NET
cd ..\TicketsAPI
dotnet publish -c Release -o publish

# 4. Verificar archivos
Write-Host "Archivos Angular en: Plantilla_Angular/dist"
Write-Host "Archivos .NET en: TicketsAPI/publish"
```

## 🚀 Deploy a Azure (Ejemplo)

```powershell
# Si usas Azure CLI
az login

# Deploy API
cd TicketsAPI
az webapp deployment source config-zip `
  --resource-group <tu-resource-group> `
  --name <tu-app-name> `
  --src publish.zip

# Deploy Frontend (Static Web App)
cd ..\Plantilla_Angular
az staticwebapp deploy `
  --name <tu-static-app-name> `
  --app-location "dist"
```

## 📊 Análisis de Performance

```powershell
# Analizar bundle de Angular
cd Plantilla_Angular
npm run build -- --configuration production --stats-json
npx webpack-bundle-analyzer dist/stats.json
```

## 🔄 Reinstalación Completa (si algo falla)

```powershell
# Angular
cd Plantilla_Angular
Remove-Item -Recurse -Force node_modules
Remove-Item package-lock.json
npm install

# .NET
cd ..\TicketsAPI
dotnet clean
dotnet restore
dotnet build
```

## 🐛 Troubleshooting

### Error: "Budget exceeded"
```powershell
# Ya está solucionado en angular.json, pero si persiste:
cd Plantilla_Angular
# Editar angular.json y aumentar budgets manualmente
```

### Error: TypeScript compilation errors
```powershell
# Ver errores detallados
cd Plantilla_Angular
npx tsc --noEmit --pretty

# Si hay errores de tipos any, temporalmente:
# En tsconfig.json, cambiar "strict": false (NO RECOMENDADO)
```

### Error: CORS en API
```powershell
# Verificar Program.cs línea ~15-35
# Asegurar que el origen del frontend esté en la lista
```

## 📝 Git Commands Post-Optimización

```powershell
# Ver archivos cambiados
git status

# Ver diferencias
git diff

# Commit optimizaciones
git add .
git commit -m "feat: optimizaciones de performance completas"

# Push a repositorio
git push origin main
```

## 🔍 Verificar .gitignore

```powershell
# Ver qué archivos están siendo ignorados
git status --ignored

# Eliminar archivos que ya están en .gitignore del tracking
git rm -r --cached TicketsAPI/publish
git rm -r --cached TicketsAPI/bin
git rm -r --cached TicketsAPI/obj
git commit -m "chore: eliminar build artifacts del repositorio"
```

## ⚙️ Configuración de VS Code (Opcional)

Crear `.vscode/settings.json`:
```json
{
  "editor.formatOnSave": true,
  "editor.codeActionsOnSave": {
    "source.fixAll.eslint": true
  },
  "typescript.preferences.importModuleSpecifier": "relative",
  "files.exclude": {
    "**/.angular": true,
    "**/node_modules": true,
    "**/bin": true,
    "**/obj": true
  }
}
```

## 📊 Métricas a Verificar

```powershell
# Tamaño del bundle Angular
cd Plantilla_Angular/dist
Get-ChildItem -Recurse *.js | Measure-Object -Property Length -Sum

# Número de archivos en publish
cd ../../TicketsAPI/publish
(Get-ChildItem -Recurse).Count
```
