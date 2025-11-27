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
            var allowedOrigins = new[] {
                "http://localhost:4200",                                                    // Desarrollo local
                "https://visual-code-proyectos-h3kq0o0km-onunezs-projects.vercel.app",     // Producción Vercel
                "https://siat-provincias.azurewebsites.net",                                 // Producción Azure (legacy)
                "https://onunez2025.github.io"                                               // GitHub Pages
            };

            if (builder.Environment.IsDevelopment())
            {
                corsBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
            else
            {
                corsBuilder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }
        });
});

// Agregar compresión para mejorar performance
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
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

// Habilitar compresión
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets API V1");
        c.RoutePrefix = string.Empty; // Swagger en la raíz (/)
    });
}
else
{
    // En producción, Swagger disponible pero no en la raíz
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets API V1");
    });
}

// Habilitar CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

// Health check simple
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.Run();
