using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgreSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GAC_APP_TB_MATERIALES",
                columns: table => new
                {
                    ID_Material = table.Column<string>(type: "text", nullable: false),
                    ID_Externo = table.Column<string>(type: "text", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Categoria = table.Column<string>(type: "text", nullable: true),
                    Unidad_medida = table.Column<string>(type: "text", nullable: true),
                    Uso = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Garantia = table.Column<string>(type: "text", nullable: true),
                    Sector = table.Column<string>(type: "text", nullable: true),
                    Descuento = table.Column<string>(type: "text", nullable: true),
                    EstadoEnCatalogo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAC_APP_TB_MATERIALES", x => x.ID_Material);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_CAS",
                columns: table => new
                {
                    ID_cas = table.Column<string>(type: "text", nullable: false),
                    Ruc = table.Column<string>(type: "text", nullable: true),
                    Razon_social = table.Column<string>(type: "text", nullable: true),
                    Pais_Region = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    Celular_2 = table.Column<string>(type: "text", nullable: true),
                    Telefono_fijo = table.Column<string>(type: "text", nullable: true),
                    Correo_electronico = table.Column<string>(type: "text", nullable: true),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_CAS", x => x.ID_cas);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_CLIENTES",
                columns: table => new
                {
                    ID_cliente = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Apellido = table.Column<string>(type: "text", nullable: true),
                    Pais_Region = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true),
                    Nombre_via = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Nombre_zona = table.Column<string>(type: "text", nullable: true),
                    Referencia = table.Column<string>(type: "text", nullable: true),
                    Referencia_adicional = table.Column<string>(type: "text", nullable: true),
                    Nro_int_dpto = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    Celular_2 = table.Column<string>(type: "text", nullable: true),
                    Telefono_fijo = table.Column<string>(type: "text", nullable: true),
                    Correo_electronico = table.Column<string>(type: "text", nullable: true),
                    Pais_region_fiscal = table.Column<string>(type: "text", nullable: true),
                    Nacionalidad = table.Column<string>(type: "text", nullable: true),
                    Tipo_documento = table.Column<string>(type: "text", nullable: true),
                    Numero_documento = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true),
                    Moroso = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_CLIENTES", x => x.ID_cliente);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_DEPARTAMENTO",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_DEPARTAMENTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_DISTRITO",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_DISTRITO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_EMPLEADOS",
                columns: table => new
                {
                    ID_empleado = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Apellido = table.Column<string>(type: "text", nullable: true),
                    Tipo_documento = table.Column<string>(type: "text", nullable: true),
                    Numero_documento = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    Correo_electronico = table.Column<string>(type: "text", nullable: true),
                    Cargo = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_EMPLEADOS", x => x.ID_empleado);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_INFORME_TECNICO",
                columns: table => new
                {
                    ID_informe_tecnico = table.Column<string>(type: "text", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: true),
                    Visita_realizada = table.Column<string>(type: "text", nullable: true),
                    Trabajo_efectuado = table.Column<string>(type: "text", nullable: true),
                    Observaciones_tecnico = table.Column<string>(type: "text", nullable: true),
                    Serie_producto = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Firma_tecnico = table.Column<string>(type: "text", nullable: true),
                    Firma_cliente = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_INFORME_TECNICO", x => x.ID_informe_tecnico);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_PRODUCTO_REGISTRADO",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Cliente = table.Column<string>(type: "text", nullable: true),
                    Producto = table.Column<string>(type: "text", nullable: true),
                    Categoria_producto_registrado = table.Column<string>(type: "text", nullable: true),
                    Pais_region = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true),
                    Edificio_planta_sala = table.Column<string>(type: "text", nullable: true),
                    Nombre_via = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Lugar_compra = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Nombre_zona = table.Column<string>(type: "text", nullable: true),
                    Referencia = table.Column<string>(type: "text", nullable: true),
                    Garantia = table.Column<string>(type: "text", nullable: true),
                    Latitud = table.Column<string>(type: "text", nullable: true),
                    Longitud = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_PRODUCTO_REGISTRADO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_PROVINCIA",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_PROVINCIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GACP_APP_TB_TICKETS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    Empresa = table.Column<string>(type: "text", nullable: true),
                    Cliente = table.Column<string>(type: "text", nullable: true),
                    Celular = table.Column<string>(type: "text", nullable: true),
                    Celula_2 = table.Column<string>(type: "text", nullable: true),
                    Telefono_fijo = table.Column<string>(type: "text", nullable: true),
                    Correo_promotor_venta = table.Column<string>(type: "text", nullable: true),
                    Fecha_visita = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Inicio_solicitado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Fin_solicitado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Productosregistrados = table.Column<string>(name: "Productos registrados", type: "text", nullable: true),
                    Pais_region = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true),
                    Nombre_via = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Nombre_zona = table.Column<string>(type: "text", nullable: true),
                    Referencia = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Tipo_servicio = table.Column<string>(type: "text", nullable: true),
                    Motivo_incidente = table.Column<string>(type: "text", nullable: true),
                    Tecnico = table.Column<string>(type: "text", nullable: true),
                    Ayudante = table.Column<string>(type: "text", nullable: true),
                    Creado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Creado_por = table.Column<string>(type: "text", nullable: true),
                    Modificado_el = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modificado_por = table.Column<string>(type: "text", nullable: true),
                    Check_in = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Check_in_localizacion = table.Column<string>(type: "text", nullable: true),
                    Notas_internas = table.Column<string>(type: "text", nullable: true),
                    IT = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GACP_APP_TB_TICKETS", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GAC_APP_TB_MATERIALES");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_CAS");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_CLIENTES");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_DEPARTAMENTO");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_DISTRITO");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_EMPLEADOS");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_INFORME_TECNICO");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_PRODUCTO_REGISTRADO");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_PROVINCIA");

            migrationBuilder.DropTable(
                name: "GACP_APP_TB_TICKETS");
        }
    }
}
