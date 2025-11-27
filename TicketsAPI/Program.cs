using Microsoft.EntityFrameworkCore;
using TicketsAPI.Data;
using TicketsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IOneDriveService, OneDriveService>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar CORS para Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        corsBuilder =>
        {
            corsBuilder
                .WithOrigins(
                    "http://localhost:4200",                                    // Desarrollo local
                    "https://siat-provincias.azurewebsites.net",                // Producción Azure
                    "https://onunez2025.github.io"                              // GitHub Pages
                )
                .SetIsOriginAllowedToAllowWildcardSubdomains()                  // Permite subdominios
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();

            // En desarrollo, permitir cualquier origen
            if (builder.Environment.IsDevelopment())
            {
                corsBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Tickets API",
        Version = "v1",
        Description = "API para gestión de tickets - Solo lectura"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// Habilitar Swagger en todos los ambientes (útil para Railway)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets API V1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz (/)
});

// Railway maneja HTTPS en su proxy, no necesitamos redirección aquí
// app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
