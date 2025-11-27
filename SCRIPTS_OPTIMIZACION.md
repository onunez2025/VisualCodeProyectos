# Scripts de Optimización - Sistema de Tickets

## Build de Producción Angular

### Windows (PowerShell)
```powershell
cd Plantilla_Angular
npm run build
```

### Verificar tamaño del bundle
```powershell
cd Plantilla_Angular/dist
Get-ChildItem -Recurse | Measure-Object -Property Length -Sum | Select-Object @{Name="Size(MB)";Expression={[math]::Round($_.Sum/1MB,2)}}
```

## Build de Producción .NET

### Windows (PowerShell)
```powershell
cd TicketsAPI
dotnet publish -c Release -o publish
```

## Limpieza de Archivos Temporales

### Angular
```powershell
cd Plantilla_Angular
Remove-Item -Recurse -Force .angular -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force dist -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force node_modules -ErrorAction SilentlyContinue
npm install
```

### .NET
```powershell
cd TicketsAPI
Remove-Item -Recurse -Force bin -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force obj -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force publish -ErrorAction SilentlyContinue
dotnet restore
```

## Análisis de Bundle (Angular)

### Instalar webpack-bundle-analyzer
```powershell
cd Plantilla_Angular
npm install --save-dev webpack-bundle-analyzer
```

### Generar reporte
```powershell
ng build --configuration production --stats-json
npx webpack-bundle-analyzer dist/stats.json
```

## Optimización de Imágenes

### Instalar imagemin
```powershell
npm install -g imagemin-cli imagemin-pngquant imagemin-mozjpeg
```

### Optimizar todas las imágenes
```powershell
cd Plantilla_Angular/src/assets/images
imagemin *.jpg --plugin=mozjpeg > optimized/
imagemin *.png --plugin=pngquant > optimized/
```

## Verificar Performance

### Lighthouse (Chrome DevTools)
1. Build de producción: `ng build --configuration production`
2. Servir: `npx http-server dist -p 8080`
3. Abrir Chrome DevTools > Lighthouse
4. Ejecutar audit

### Métricas Objetivo
- Performance: >90
- Accessibility: >90
- Best Practices: >90
- SEO: >90

## Test antes de Deploy

### Angular
```powershell
cd Plantilla_Angular
npm test
npm run lint
npm run build -- --configuration production
```

### .NET
```powershell
cd TicketsAPI
dotnet test
dotnet build -c Release
```

## Deploy

### Azure (desde PowerShell)
```powershell
# API
cd TicketsAPI
az webapp deployment source config-zip --resource-group <resource-group> --name <app-name> --src publish.zip

# Frontend (si usa Azure Static Web Apps)
cd Plantilla_Angular
npm run build
az staticwebapp deploy --name <app-name> --source dist/
```

## Comandos Útiles

### Ver tamaño de carpetas
```powershell
Get-ChildItem | Where-Object {$_.PSIsContainer} | ForEach-Object {
    $size = (Get-ChildItem $_.FullName -Recurse | Measure-Object -Property Length -Sum).Sum
    [PSCustomObject]@{
        Folder = $_.Name
        'Size(MB)' = [math]::Round($size/1MB,2)
    }
} | Sort-Object 'Size(MB)' -Descending
```

### Limpiar completamente
```powershell
# Ejecutar desde la raíz del proyecto
Remove-Item -Recurse -Force Plantilla_Angular/.angular -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force Plantilla_Angular/dist -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force Plantilla_Angular/node_modules -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force TicketsAPI/bin -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force TicketsAPI/obj -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force TicketsAPI/publish -ErrorAction SilentlyContinue
```
