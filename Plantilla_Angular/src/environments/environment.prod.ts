import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  apiUrl: 'https://visualcodeproyectos-production.up.railway.app/api'
};
