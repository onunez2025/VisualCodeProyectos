using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TicketsAPI.Data;

namespace TicketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HealthController> _logger;

        public HealthController(
            ApplicationDbContext context, 
            IConfiguration configuration,
            ILogger<HealthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running", timestamp = DateTime.UtcNow });
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                _logger.LogInformation("Probando conexión a base de datos...");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    _logger.LogInformation("Conexión abierta exitosamente");
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(*) FROM [dbo].[GACP_APP_TB_TICKETS]";
                        var count = await command.ExecuteScalarAsync();
                        
                        _logger.LogInformation("Query ejecutado. Tickets encontrados: {Count}", count);
                        
                        return Ok(new 
                        { 
                            status = "Database connection successful",
                            totalTickets = count,
                            server = connection.DataSource,
                            database = connection.Database
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al conectar a la base de datos: {Message}", ex.Message);
                return StatusCode(500, new 
                { 
                    status = "Database connection failed",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpGet("clientes-structure")]
        public async Task<IActionResult> GetClientesStructure()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GACP_APP_TB_CLIENTES' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("clientes-sample")]
        public async Task<IActionResult> GetClientesSample()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT TOP 3 * FROM [dbo].[GACP_APP_TB_CLIENTES]";
                        
                        var clientes = new List<Dictionary<string, object>>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var cliente = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    cliente[reader.GetName(i)] = reader.IsDBNull(i) ? (object)"NULL" : reader.GetValue(i);
                                }
                                clientes.Add(cliente);
                            }
                        }
                        
                        return Ok(clientes);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("materiales-structure")]
        public async Task<IActionResult> GetMaterialesStructure()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GAC_APP_TB_MATERIALES' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("productos-registrados-structure")]
        public async Task<IActionResult> GetProductosRegistradosStructure()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GACP_APP_TB_PRODUCTO_REGISTRADO' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("empleados-structure")]
        public async Task<IActionResult> GetEmpleadosStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GACP_APP_TB_EMPLEADOS' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("cas-structure")]
        public async Task<IActionResult> GetCasStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GACP_APP_TB_CAS' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("informe-tecnico-structure")]
        public async Task<IActionResult> GetInformeTecnicoStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'GACP_APP_TB_INFORME_TECNICO' 
                            ORDER BY ORDINAL_POSITION";
                        
                        var columns = new List<object>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                columns.Add(new
                                {
                                    columnName = reader.GetString(0),
                                    dataType = reader.GetString(1),
                                    maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)
                                });
                            }
                        }
                        
                        return Ok(columns);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("departamento-structure")]
        public async Task<IActionResult> GetDepartamentoStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT TOP 10 * FROM [dbo].[GACP_APP_TB_DEPARTAMENTO]";
                        
                        var results = new List<Dictionary<string, object>>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                        }
                        
                        return Ok(results);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("provincia-structure")]
        public async Task<IActionResult> GetProvinciaStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT TOP 10 * FROM [dbo].[GACP_APP_TB_PROVINCIA]";
                        
                        var results = new List<Dictionary<string, object>>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                        }
                        
                        return Ok(results);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("distrito-structure")]
        public async Task<IActionResult> GetDistritoStructure()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT TOP 10 * FROM [dbo].[GACP_APP_TB_DISTRITO]";
                        
                        var results = new List<Dictionary<string, object>>();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                        }
                        
                        return Ok(results);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
