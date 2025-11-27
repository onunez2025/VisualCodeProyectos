using Microsoft.AspNetCore.Mvc;

namespace TicketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IConfiguration _configuration;

        public FilesController(ILogger<FilesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Sirve archivos desde OneDrive local sincronizado
        /// GET /api/files/OS_EC48ECB0.pdf
        /// GET /api/files/informe/IMG_20250101_123456.jpg
        /// </summary>
        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            try
            {
                // Rutas configuradas en appsettings.json
                var filesPath = _configuration["OneDrive:FilesPath"] ?? 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "OneDrive - MT INDUSTRIAL S.A.C (1)",
                        "appsheet", "data", "SIATP-266763730", "appsheet", "data", "SIATP-266763730", "Files"
                    );

                var informeFilesPath = _configuration["OneDrive:InformeTecnicoFilesPath"] ?? 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "OneDrive - MT INDUSTRIAL S.A.C (1)",
                        "appsheet", "data", "SIATP-266763730", "INFORME_TECNICO_Files_"
                    );

                var informePath = _configuration["OneDrive:InformeTecnicoImagesPath"] ?? 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "OneDrive - MT INDUSTRIAL S.A.C (1)",
                        "appsheet", "data", "SIATP-266763730", "INFORME_TECNICO_Images"
                    );

                // Buscar el archivo en las rutas conocidas (Files primero para PDFs)
                var possiblePaths = new[]
                {
                    Path.Combine(filesPath, fileName),
                    Path.Combine(informeFilesPath, fileName),
                    Path.Combine(informePath, fileName)
                };

                string? filePath = null;
                foreach (var path in possiblePaths)
                {
                    if (System.IO.File.Exists(path))
                    {
                        filePath = path;
                        break;
                    }
                }

                if (filePath == null)
                {
                    _logger.LogWarning("Archivo no encontrado: {FileName}", fileName);
                    return NotFound(new { message = $"Archivo {fileName} no encontrado" });
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = GetContentType(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener archivo: {FileName}", fileName);
                return StatusCode(500, new { message = "Error al obtener el archivo", error = ex.Message });
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };
        }

        /// <summary>
        /// Obtiene una vista previa/miniatura del archivo
        /// </summary>
        [HttpGet("{fileName}/preview")]
        public IActionResult GetFilePreview(string fileName)
        {
            // Implementación para miniaturas si es necesario
            return GetFile(fileName);
        }

        /// <summary>
        /// Sirve imágenes del informe técnico
        /// GET /api/files/informe/IMG_20250101_123456.jpg
        /// </summary>
        [HttpGet("informe/{fileName}")]
        public IActionResult GetInformeImage(string fileName)
        {
            try
            {
                var informePath = _configuration["OneDrive:InformeTecnicoImagesPath"] ?? 
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "OneDrive - MT INDUSTRIAL S.A.C (1)",
                        "appsheet", "data", "SIATP-266763730", "INFORME_TECNICO_Images"
                    );

                var filePath = Path.Combine(informePath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    _logger.LogWarning("Imagen de informe no encontrada: {FileName}", fileName);
                    return NotFound(new { message = $"Imagen {fileName} no encontrada" });
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var contentType = GetContentType(fileName);

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener imagen de informe: {FileName}", fileName);
                return StatusCode(500, new { message = "Error al obtener la imagen", error = ex.Message });
            }
        }
    }
}
