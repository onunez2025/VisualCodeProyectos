import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  // 🚀 IMPORTANTE: Cambiar esta URL después de desplegar en Railway
  // Ejemplo: apiUrl: 'https://tu-app-railway.railway.app/api'
  apiUrl: 'http://localhost:5270/api' // Temporal - actualizar con Railway URL
};
