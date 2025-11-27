# 🌐 Paso 2: Crear Repositorio en GitHub y Subir Código

## 📋 Resumen
Tu código está listo localmente. Ahora vamos a subirlo a GitHub.

---

## 🎯 PASO 1: Crear Repositorio en GitHub

### 1.1 Abre tu Navegador

Ve a esta URL:
```
https://github.com/new
```

O sigue estos pasos:
1. Ve a https://github.com
2. Inicia sesión con:
   - **Usuario:** `onunez2025`
   - **Contraseña:** `Sole12345`
3. Click en el botón **"+"** (arriba a la derecha)
4. Click en **"New repository"**

### 1.2 Configurar el Repositorio

En la página de creación, llena así:

**Owner:** `onunez2025` (ya debe estar seleccionado)

**Repository name:** 
```
VisualCodeProyectos
```

**Description** (opcional):
```
Sistema de Tickets - Backend .NET 8 + Frontend Angular 20
```

**Visibility:**
- ✅ **Public** (recomendado para GitHub Pages gratuito)
- O **Private** (si prefieres que sea privado)

**IMPORTANTE - NO marques:**
- ❌ NO marques "Add a README file"
- ❌ NO marques "Add .gitignore"
- ❌ NO marques "Choose a license"

(Déjalo todo sin marcar)

### 1.3 Crear

Click en el botón verde **"Create repository"**

---

## 🔑 PASO 2: Crear Personal Access Token

GitHub ya no acepta contraseñas. Necesitas un token:

### 2.1 Ir a Settings

1. En GitHub, click en tu foto (arriba derecha)
2. Click **"Settings"**
3. Scroll hasta abajo en el menú izquierdo
4. Click **"Developer settings"**
5. Click **"Personal access tokens"**
6. Click **"Tokens (classic)"**

O ve directamente a:
```
https://github.com/settings/tokens
```

### 2.2 Generar Token

1. Click **"Generate new token"** → **"Generate new token (classic)"**

2. Llena el formulario:
   - **Note:** `VisualCodeProyectos Deployment`
   - **Expiration:** `90 days` (o lo que prefieras)
   - **Select scopes:** Marca **✅ repo** (el primero, completo)

3. Scroll abajo y click **"Generate token"**

4. **MUY IMPORTANTE:**
   - Te mostrará un token verde que empieza con `ghp_...`
   - **COPIA ESE TOKEN** (solo se muestra una vez)
   - Guárdalo en un lugar seguro (notepad)

**Ejemplo de token:**
```
ghp_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```

---

## 🚀 PASO 3: Conectar y Subir el Código

### 3.1 Volver al Símbolo del Sistema (cmd)

En la misma ventana del Símbolo del sistema donde ejecutaste los comandos anteriores.

### 3.2 Conectar con GitHub

Ejecuta este comando (reemplaza `onunez2025` si tu usuario es diferente):

```cmd
git remote add origin https://github.com/onunez2025/VisualCodeProyectos.git
```

### 3.3 Renombrar Branch a Main

```cmd
git branch -M main
```

### 3.4 Subir el Código

```cmd
git push -u origin main
```

**Te pedirá credenciales:**

**Username for 'https://github.com':** 
```
onunez2025
```

**Password for 'https://onunez2025@github.com':**
```
ghp_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```
(Pega aquí el token que copiaste, NO la contraseña `Sole12345`)

**IMPORTANTE:** Cuando pegues el token, no se verá nada en pantalla (es normal por seguridad). Solo pégalo y presiona Enter.

---

## ✅ Verificación

Después del `git push`, deberías ver algo como:

```
Enumerating objects: XXX, done.
Counting objects: 100% (XXX/XXX), done.
Delta compression using up to X threads
Compressing objects: 100% (XXX/XXX), done.
Writing objects: 100% (XXX/XXX), XXX KiB | XXX MiB/s, done.
Total XXX (delta XX), reused 0 (delta 0)
remote: Resolving deltas: 100% (XX/XX), done.
To https://github.com/onunez2025/VisualCodeProyectos.git
 * [new branch]      main -> main
Branch 'main' set up to track remote branch 'main' from 'origin'.
```

### Confirmar en GitHub

1. Ve a: https://github.com/onunez2025/VisualCodeProyectos
2. Deberías ver todos tus archivos
3. Deberías ver el commit "Initial commit - Sistema de Tickets"

---

## 📞 Avísame Cuando:

✅ Hayas creado el repositorio en GitHub
✅ Hayas generado el Personal Access Token
✅ Hayas ejecutado el `git push` exitosamente
✅ O si hay algún error, envíame un screenshot

**Próximo paso:** Una vez que el código esté en GitHub, configuraremos Railway para el backend. 🚀
