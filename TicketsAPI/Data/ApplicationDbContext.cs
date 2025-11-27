using Microsoft.EntityFrameworkCore;
using TicketsAPI.Models;

namespace TicketsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<ProductoRegistrado> ProductosRegistrados { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cas> CasEmpresas { get; set; }
        public DbSet<InformeTecnico> InformesTecnicos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Distrito> Distritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("GACP_APP_TB_TICKETS", "dbo");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("GACP_APP_TB_CLIENTES", "dbo");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).HasColumnName("ID_cliente");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("GAC_APP_TB_MATERIALES", "dbo");
                entity.HasKey(e => e.IdMaterial);
                entity.Property(e => e.IdMaterial).HasColumnName("ID_Material");
            });

            modelBuilder.Entity<ProductoRegistrado>(entity =>
            {
                entity.ToTable("GACP_APP_TB_PRODUCTO_REGISTRADO", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
            });
        }
    }
}
