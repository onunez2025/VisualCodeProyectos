using System.Web;

namespace TicketsAPI.Services
{
    public interface IOneDriveService
    {
        string ConvertToPublicUrl(string? oneDrivePath);
    }

    public class OneDriveService : IOneDriveService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OneDriveService> _logger;

        public OneDriveService(IConfiguration configuration, ILogger<OneDriveService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Convierte una ruta de OneDrive a una URL pública accesible
        /// Ejemplo: "appsheet/data/SIATP-266763730/Files/OS_EC48ECB0.pdf" 
        /// -> URL de OneDrive compartido
        /// </summary>
        public string ConvertToPublicUrl(string? oneDrivePath)
        {
            if (string.IsNullOrWhiteSpace(oneDrivePath))
            {
                return string.Empty;
            }

            try
            {
                // Opción 1: Si tienes un enlace base de OneDrive compartido
                var baseUrl = _configuration["OneDrive:BaseUrl"];
                
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    // Combinar la URL base con la ruta del archivo
                    var fileName = Path.GetFileName(oneDrivePath);
                    return $"{baseUrl}/{HttpUtility.UrlEncode(fileName)}";
                }

                // Opción 2: Si las imágenes están en una carpeta específica de tu OneDrive
                // Reemplaza con la estructura real de tu OneDrive
                var oneDriveRoot = _configuration["OneDrive:RootPath"] ?? 
                    "https://mtindustrialsac-my.sharepoint.com/personal/onunez_mtindustrial_sac/_layouts/15/download.aspx?share=";

                // Opción 3: Usar la ruta local de OneDrive si está sincronizado
                var localOneDrivePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "OneDrive - MT INDUSTRIAL S.A.C (1)",
                    oneDrivePath.Replace("/", "\\")
                );

                if (File.Exists(localOneDrivePath))
                {
                    // Si el archivo existe localmente, podrías servirlo a través de un endpoint
                    return $"/api/files/{HttpUtility.UrlEncode(Path.GetFileName(oneDrivePath))}";
                }

                // Retornar la ruta original si no se puede convertir
                return oneDrivePath;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "No se pudo convertir la ruta de OneDrive: {Path}", oneDrivePath);
                return oneDrivePath;
            }
        }
    }
}
