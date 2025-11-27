# 🚀 Comandos Git - Ejecutar Manualmente

## ⚠️ Situación

Git aún no es reconocido por PowerShell. Vamos a usar **Git Bash** directamente o verificar la instalación.

---

## ✅ SOLUCIÓN 1: Usar Git Bash (Más Confiable)

### Paso 1: Abrir Git Bash

1. Presiona `Windows + R`
2. Escribe: **`git-bash`** o busca **"Git Bash"** en el menú inicio
3. Presiona Enter

### Paso 2: Navegar a tu Proyecto

En Git Bash, copia y pega este comando:

```bash
cd "/c/Users/onunez/OneDrive - MT INDUSTRIAL S.A.C/Escritorio/VisualCodeProyectos"
```

### Paso 3: Configurar Git

```bash
git config --global user.name "onunez2025"
git config --global user.email "onunez@sole.com.pe"
```

### Paso 4: Inicializar Repositorio

```bash
git init
```

### Paso 5: Agregar Archivos

```bash
git add .
```

### Paso 6: Hacer Commit

```bash
git commit -m "Initial commit - Sistema de Tickets"
```

---

## ✅ SOLUCIÓN 2: Verificar Instalación de Git

Si Git Bash no abre, puede que Git no se instaló correctamente.

### Opción A: Reinstalar Git

1. Ve a: https://git-scm.com/download/win
2. Descarga nuevamente
3. Durante la instalación, asegúrate de seleccionar:
   - ✅ **"Git from the command line and also from 3rd-party software"**
   - ✅ **"Use Windows' default console window"**
4. Completa la instalación
5. Reinicia completamente tu computadora (no solo VS Code)

### Opción B: Agregar Git al PATH Manualmente

1. Abre el Explorador de Archivos
2. Navega a: `C:\Program Files\Git\bin`
3. Copia esa ruta
4. Presiona `Windows + R`
5. Escribe: `sysdm.cpl`
6. Ve a **"Variables de entorno"**
7. En **"Variables del sistema"**, busca **"Path"**
8. Click **"Editar"**
9. Click **"Nuevo"**
10. Pega: `C:\Program Files\Git\bin`
11. Click **OK** en todas las ventanas
12. **Reinicia VS Code completamente**

---

## 🎯 Siguiente Paso: Crear Repositorio en GitHub

Una vez que hayas ejecutado los comandos de Git exitosamente, necesitas:

### 1. Crear el Repositorio en GitHub

1. Ve a: https://github.com/new
2. **Repository name**: `VisualCodeProyectos`
3. **Visibility**: Public (o Private si prefieres)
4. **NO marques** "Add a README file"
5. Click **"Create repository"**

### 2. Conectar y Subir

GitHub te mostrará comandos. Usa estos en Git Bash:

```bash
git remote add origin https://github.com/onunez2025/VisualCodeProyectos.git
git branch -M main
git push -u origin main
```

**IMPORTANTE:** Cuando pida credenciales, va a pedir un **Token** en lugar de contraseña.

---

## 🔑 Crear Personal Access Token (GitHub)

1. Ve a: https://github.com/settings/tokens
2. Click **"Generate new token"** → **"Generate new token (classic)"**
3. **Note**: "VisualCodeProyectos Deployment"
4. **Expiration**: 90 days (o lo que prefieras)
5. Marca el scope: **✅ repo** (completo)
6. Scroll abajo y click **"Generate token"**
7. **COPIA EL TOKEN** (aparece en verde, solo sale una vez)
8. Úsalo como "contraseña" cuando hagas `git push`

**Usuario:** `onunez2025`
**Contraseña:** `ghp_xxxxxxxxxxxxxxxxxxxxxxxxx` (tu token)

---

## 📞 Avísame Cuando...

1. ✅ Git Bash funcione y veas algo como `git version 2.x.x`
2. ✅ Hayas hecho el commit inicial exitosamente
3. ✅ Hayas creado el repositorio en GitHub
4. ✅ Tengas tu Personal Access Token

Entonces te ayudaré con:
- Subir el código a GitHub
- Configurar Railway
- Publicar el frontend

---

## 💡 Verificación Rápida

En Git Bash, ejecuta:

```bash
git --version
```

Deberías ver: `git version 2.x.x.windows.x`

Si ves eso, **¡Git funciona!** Continúa con los pasos de arriba.

---

**¿Qué opción prefieres?**
- A) Usar Git Bash directamente
- B) Reinstalar Git
- C) Agregar Git al PATH manualmente

Avísame y te guío paso a paso. 🚀
