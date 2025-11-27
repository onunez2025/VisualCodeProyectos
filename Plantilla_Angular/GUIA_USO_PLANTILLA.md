# 📘 Guía de Uso - Plantilla Datta Able Angular

## 🎯 Resumen de la Plantilla

**Datta Able** es una plantilla de administración Angular 20 (compatible con versiones anteriores) con Bootstrap 5, diseñada para crear dashboards y paneles administrativos profesionales.

---

## 📦 Instalación y Configuración

### 1. Instalar dependencias
```bash
npm install
# o
yarn install
```

### 2. Iniciar el proyecto en desarrollo
```bash
npm start
# o
yarn start
```

### 3. Compilar para producción
```bash
npm run build
# o
yarn build
```

---

## 🏗️ Estructura del Proyecto

### Arquitectura Principal

```
src/app/
├── app-routing.module.ts          # Rutas principales
├── app.component.ts                # Componente raíz
├── demo/                           # Ejemplos y componentes de demostración
│   ├── dashboard/                  # Página dashboard
│   ├── pages/                      # Páginas (auth, forms, tables, charts)
│   └── ui-elements/                # Componentes UI
├── theme/                          # Sistema de temas
│   ├── layout/                     # Layouts
│   │   ├── admin/                  # Layout para usuarios autenticados
│   │   └── guest/                  # Layout para páginas públicas (login, registro)
│   └── shared/                     # Componentes compartidos
│       ├── components/             # Card, breadcrumbs, spinner
│       └── shared.module.ts        # Módulo de componentes reutilizables
└── assets/                         # Recursos estáticos
    ├── images/
    ├── charts/
    └── icon/
```

---

## 🎨 Sistema de Layouts

La plantilla usa **2 layouts principales**:

### 1. **AdminComponent** (`theme/layout/admin/`)
Para páginas autenticadas (dashboard, tablas, formularios)
- Incluye: Navbar, Sidebar, Footer, Breadcrumbs
- Configuración en `app-routing.module.ts`:

```typescript
{
  path: '',
  component: AdminComponent,
  children: [
    {
      path: 'dashboard',
      loadComponent: () => import('./demo/dashboard/dashboard.component')
    },
    // Tus páginas privadas aquí
  ]
}
```

### 2. **GuestComponent** (`theme/layout/guest/`)
Para páginas públicas (login, registro)
- Layout simple sin sidebar
- Configuración:

```typescript
{
  path: '',
  component: GuestComponent,
  children: [
    {
      path: 'login',
      loadComponent: () => import('./demo/pages/authentication/auth-signin/auth-signin.component')
    },
    // Tus páginas públicas aquí
  ]
}
```

---

## 🧩 Componentes Principales

### SharedModule (`theme/shared/shared.module.ts`)
Módulo que exporta componentes reutilizables:
- `CardComponent` - Tarjetas estilizadas
- `CommonModule`, `FormsModule`, `ReactiveFormsModule`
- `NgbModule` - Bootstrap para Angular
- `NgScrollbarModule` - Scrollbars personalizados

**Uso:** Importa `SharedModule` en tus componentes:

```typescript
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  imports: [CommonModule, SharedModule],
  // ...
})
```

### Componente Card

```html
<app-card cardTitle="Mi Título" [options]="false">
  <!-- Tu contenido aquí -->
</app-card>
```

---

## 🧭 Sistema de Navegación

### Configuración del Menú (`theme/layout/admin/navigation/navigation.ts`)

```typescript
export const NavigationItems: NavigationItem[] = [
  {
    id: 'navigation',
    title: 'Navigation',
    type: 'group',           // Tipo: 'group' | 'item' | 'collapse'
    children: [
      {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'item',
        url: '/dashboard',
        icon: 'feather icon-home',
        classes: 'nav-item'
      }
    ]
  },
  // Más grupos...
];
```

### Tipos de Items de Navegación:
- **`group`**: Agrupa múltiples items (ej: "UI ELEMENT")
- **`item`**: Enlace directo a una página
- **`collapse`**: Submenú desplegable

### Añadir tu propio menú:

```typescript
{
  id: 'mi-modulo',
  title: 'Mi Módulo',
  type: 'group',
  children: [
    {
      id: 'mi-pagina',
      title: 'Mi Página',
      type: 'item',
      url: '/mi-pagina',
      icon: 'feather icon-layers',
      classes: 'nav-item'
    }
  ]
}
```

---

## 🎨 Estilos y Temas

### Archivo Principal (`src/styles.scss`)

```scss
@import 'scss/variables';              // Variables globales
@import 'scss/fonts/fontawesome/scss/fontawesome';
@import 'scss/fonts/feather/iconfont'; // Iconos Feather
@import 'scss/general';                // Estilos generales
@import 'scss/menu/menu-lite';         // Estilos del menú
@import 'scss/theme-elements/theme-elements'; // Botones, forms, etc.
@import 'scss/plugins/plugins';        // Plugins externos
@import 'scss/custom';                 // ⭐ TUS ESTILOS PERSONALIZADOS
```

### Personalizar Variables (`src/scss/_variables.scss`)
Aquí puedes cambiar colores, fuentes, espaciados, etc.

```scss
// Ejemplo:
$primary-color: #1abc9c;
$font-family: 'Roboto', sans-serif;
```

---

## 📄 Crear una Nueva Página

### Paso 1: Crear el componente

```bash
ng generate component demo/mi-modulo/mi-pagina
```

### Paso 2: Registrar la ruta

En `app-routing.module.ts`:

```typescript
{
  path: '',
  component: AdminComponent,
  children: [
    // ... rutas existentes
    {
      path: 'mi-pagina',
      loadComponent: () => import('./demo/mi-modulo/mi-pagina/mi-pagina.component')
        .then((c) => c.MiPaginaComponent)
    }
  ]
}
```

### Paso 3: Añadir al menú de navegación

En `theme/layout/admin/navigation/navigation.ts`:

```typescript
{
  id: 'mi-pagina',
  title: 'Mi Página',
  type: 'item',
  url: '/mi-pagina',
  icon: 'feather icon-star',
  classes: 'nav-item'
}
```

### Paso 4: Usar SharedModule en tu componente

```typescript
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@Component({
  selector: 'app-mi-pagina',
  standalone: true,
  imports: [CommonModule, SharedModule],
  templateUrl: './mi-pagina.component.html',
  styleUrls: ['./mi-pagina.component.scss']
})
export class MiPaginaComponent {}
```

### Paso 5: Crear la interfaz

```html
<!-- mi-pagina.component.html -->
<app-card cardTitle="Mi Página">
  <h5>¡Bienvenido a mi página!</h5>
  <p>Contenido personalizado aquí...</p>
</app-card>
```

---

## 📊 Librerías Incluidas

### UI y Componentes
- **Bootstrap 5.3.7** - Framework CSS
- **@ng-bootstrap/ng-bootstrap** - Componentes Bootstrap para Angular
- **ngx-scrollbar** - Scrollbars personalizados

### Gráficos
- **ApexCharts** - Gráficos interactivos modernos
- **ng-apexcharts** - Wrapper Angular para ApexCharts
- **AmCharts** - Gráficos y mapas (archivos en `assets/charts/amchart/`)

### Iconos
- **Feather Icons** (`feather icon-*`)
- **Font Awesome** (`fas fa-*`, `fab fa-*`)
- **Icofont** (`icofont-*`)

### Utilidades
- **screenfull** - Modo pantalla completa
- **RxJS** - Programación reactiva

---

## 🔧 Configuraciones Importantes

### Angular CLI (`angular.json`)

```json
{
  "styles": [
    "node_modules/bootstrap/scss/bootstrap.scss",  // Bootstrap
    "src/styles.scss"                               // Estilos personalizados
  ],
  "scripts": [
    "node_modules/apexcharts/dist/apexcharts.min.js" // ApexCharts global
  ]
}
```

### Ambientes (`src/environments/`)

- `environment.ts` - Desarrollo
- `environment.prod.ts` - Producción

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:3000/api'
};
```

---

## 🎯 Casos de Uso Comunes

### 1. **Dashboard con Tarjetas**

```html
<div class="row">
  <div class="col-md-6 col-xl-4" *ngFor="let stat of estadisticas">
    <app-card>
      <h6 class="mb-4">{{ stat.titulo }}</h6>
      <div class="row d-flex align-items-center">
        <div class="col-9">
          <h3 class="f-w-300 d-flex align-items-center m-b-0">
            <i class="feather {{ stat.icon }} f-30 m-r-5"></i> 
            {{ stat.valor }}
          </h3>
        </div>
      </div>
    </app-card>
  </div>
</div>
```

### 2. **Formulario con Bootstrap**

```html
<app-card cardTitle="Registro">
  <form [formGroup]="miFormulario" (ngSubmit)="onSubmit()">
    <div class="form-group">
      <label>Nombre</label>
      <input type="text" class="form-control" formControlName="nombre">
    </div>
    <div class="form-group">
      <label>Email</label>
      <input type="email" class="form-control" formControlName="email">
    </div>
    <button type="submit" class="btn btn-primary">Guardar</button>
  </form>
</app-card>
```

### 3. **Tabla Responsiva**

```html
<app-card cardTitle="Usuarios">
  <div class="table-responsive">
    <table class="table table-hover">
      <thead>
        <tr>
          <th>Nombre</th>
          <th>Email</th>
          <th>Estado</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let usuario of usuarios">
          <td>{{ usuario.nombre }}</td>
          <td>{{ usuario.email }}</td>
          <td>
            <span class="badge badge-light-success">Activo</span>
          </td>
          <td>
            <button class="btn btn-icon btn-primary btn-sm">
              <i class="feather icon-edit"></i>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</app-card>
```

---

## 🚀 Pasos para tu Proyecto

### Opción 1: Usar la plantilla completa
1. Clona el proyecto
2. Instala dependencias
3. Comienza a modificar páginas y rutas según tu necesidad

### Opción 2: Integrar en proyecto existente
1. Copia la carpeta `theme/` a tu proyecto
2. Copia los estilos de `src/scss/`
3. Importa los estilos en `angular.json`
4. Importa `SharedModule` donde lo necesites
5. Adapta las rutas y navegación

---

## 📚 Recursos Adicionales

- **Documentación oficial**: [Datta Angular Docs](https://codedthemes.gitbook.io/datta-angular/)
- **Demo en vivo**: [Ver Demo](https://codedthemes.com/demos/admin-templates/datta-able/angular/free/dashboard)
- **Bootstrap 5**: [Documentación](https://getbootstrap.com/docs/5.3/)
- **Feather Icons**: [Lista de iconos](https://feathericons.com/)
- **ApexCharts**: [Documentación](https://apexcharts.com/docs/)

---

## ⚡ Tips y Mejores Prácticas

1. **Organiza por módulos**: Crea carpetas para cada funcionalidad (usuarios, productos, reportes)
2. **Reutiliza componentes**: Usa el `SharedModule` para componentes comunes
3. **Lazy Loading**: La plantilla ya usa lazy loading, mantén esta práctica para mejor rendimiento
4. **Responsive**: Usa las clases de Bootstrap (`col-md-6`, `col-lg-4`, etc.)
5. **Iconos consistentes**: Elige una librería de iconos (Feather o FontAwesome) y úsala consistentemente

---

## 🆘 Problemas Comunes

### Error: "Module not found"
```bash
npm install
```

### Estilos no se aplican
Verifica que `styles.scss` esté importado en `angular.json`

### Menú no aparece
Revisa que estés usando `AdminComponent` como layout padre en tus rutas

---

¡Listo! Ya tienes toda la información para empezar a usar esta plantilla en tu proyecto. 🎉
