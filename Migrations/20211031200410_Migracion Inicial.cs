using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPersonas.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    GrupoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    CantidadPersonas = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.GrupoId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeAportes",
                columns: table => new
                {
                    TipoDeAporteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Meta = table.Column<float>(type: "REAL", nullable: false),
                    Logrado = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeAportes", x => x.TipoDeAporteId);
                });

            migrationBuilder.CreateTable(
                name: "GruposDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GrupoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Orden = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GruposDetalle_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Cedula = table.Column<string>(type: "TEXT", nullable: true),
                    RolId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                    table.ForeignKey(
                        name: "FK_Personas_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aportes",
                columns: table => new
                {
                    AporteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: true),
                    totalAportes = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aportes", x => x.AporteId);
                    table.ForeignKey(
                        name: "FK_Aportes_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AportesDetalle",
                columns: table => new
                {
                    AporteDetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AporteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoDeAporteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Aporte = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AportesDetalle", x => x.AporteDetalleId);
                    table.ForeignKey(
                        name: "FK_AportesDetalle_Aportes_AporteId",
                        column: x => x.AporteId,
                        principalTable: "Aportes",
                        principalColumn: "AporteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AportesDetalle_TiposDeAportes_TipoDeAporteId",
                        column: x => x.TipoDeAporteId,
                        principalTable: "TiposDeAportes",
                        principalColumn: "TipoDeAporteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aportes_PersonaId",
                table: "Aportes",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AportesDetalle_AporteId",
                table: "AportesDetalle",
                column: "AporteId");

            migrationBuilder.CreateIndex(
                name: "IX_AportesDetalle_TipoDeAporteId",
                table: "AportesDetalle",
                column: "TipoDeAporteId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposDetalle_GrupoId",
                table: "GruposDetalle",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_RolId",
                table: "Personas",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AportesDetalle");

            migrationBuilder.DropTable(
                name: "GruposDetalle");

            migrationBuilder.DropTable(
                name: "Aportes");

            migrationBuilder.DropTable(
                name: "TiposDeAportes");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
