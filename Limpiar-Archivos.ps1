# Script de Limpieza de Archivos Innecesarios
# Ejecutar desde la raíz del proyecto

Write-Host "=== LIMPIEZA DE ARCHIVOS INNECESARIOS ===" -ForegroundColor Cyan
Write-Host ""

# Función para eliminar carpeta de forma segura
function Remove-SafelyFolder {
    param($Path, $Name)
    if (Test-Path $Path) {
        Write-Host "Eliminando: $Name..." -ForegroundColor Yellow
        Remove-Item -Recurse -Force $Path -ErrorAction SilentlyContinue
        Write-Host "✓ $Name eliminado" -ForegroundColor Green
    } else {
        Write-Host "○ $Name no existe (OK)" -ForegroundColor Gray
    }
}

# Función para mostrar tamaño antes de eliminar
function Get-FolderSize {
    param($Path)
    if (Test-Path $Path) {
        $size = (Get-ChildItem $Path -Recurse -ErrorAction SilentlyContinue | Measure-Object -Property Length -Sum -ErrorAction SilentlyContinue).Sum
        return [math]::Round($size/1MB,2)
    }
    return 0
}

Write-Host "Calculando tamaños..." -ForegroundColor Cyan

# Calcular tamaños
$publishSize = Get-FolderSize "TicketsAPI\publish"
$binSize = Get-FolderSize "TicketsAPI\bin"
$objSize = Get-FolderSize "TicketsAPI\obj"
$angularCacheSize = Get-FolderSize "Plantilla_Angular\.angular"
$distSize = Get-FolderSize "Plantilla_Angular\dist"

$totalSize = $publishSize + $binSize + $objSize + $angularCacheSize + $distSize

Write-Host ""
Write-Host "Tamaños actuales:" -ForegroundColor Cyan
Write-Host "  - TicketsAPI/publish: $publishSize MB" -ForegroundColor White
Write-Host "  - TicketsAPI/bin: $binSize MB" -ForegroundColor White
Write-Host "  - TicketsAPI/obj: $objSize MB" -ForegroundColor White
Write-Host "  - Plantilla_Angular/.angular: $angularCacheSize MB" -ForegroundColor White
Write-Host "  - Plantilla_Angular/dist: $distSize MB" -ForegroundColor White
Write-Host "  TOTAL: $totalSize MB" -ForegroundColor Yellow
Write-Host ""

$response = Read-Host "¿Desea continuar con la limpieza? (S/N)"

if ($response -eq "S" -or $response -eq "s") {
    Write-Host ""
    Write-Host "Iniciando limpieza..." -ForegroundColor Green
    Write-Host ""
    
    # .NET - Carpetas de compilación
    Remove-SafelyFolder "TicketsAPI\publish" "TicketsAPI/publish"
    Remove-SafelyFolder "TicketsAPI\bin" "TicketsAPI/bin"
    Remove-SafelyFolder "TicketsAPI\obj" "TicketsAPI/obj"
    
    # Angular - Carpetas temporales
    Remove-SafelyFolder "Plantilla_Angular\.angular" "Plantilla_Angular/.angular"
    Remove-SafelyFolder "Plantilla_Angular\dist" "Plantilla_Angular/dist"
    
    Write-Host ""
    Write-Host "=== LIMPIEZA COMPLETADA ===" -ForegroundColor Green
    Write-Host "Espacio liberado: ~$totalSize MB" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Próximos pasos:" -ForegroundColor Yellow
    Write-Host "  1. Ejecutar: cd TicketsAPI; dotnet restore" -ForegroundColor White
    Write-Host "  2. Ejecutar: cd Plantilla_Angular; npm install (si es necesario)" -ForegroundColor White
    Write-Host "  3. Hacer commit de los cambios en .gitignore" -ForegroundColor White
    
} else {
    Write-Host ""
    Write-Host "Limpieza cancelada." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Nota: Los archivos node_modules NO fueron eliminados." -ForegroundColor Gray
Write-Host "Si desea reinstalar completamente Angular, ejecute:" -ForegroundColor Gray
Write-Host "  Remove-Item -Recurse -Force Plantilla_Angular\node_modules" -ForegroundColor Gray
Write-Host "  cd Plantilla_Angular; npm install" -ForegroundColor Gray
