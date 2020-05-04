using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoBlazor.Server.Migrations
{
    public partial class MigrationVotoFilme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotoFilme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFilme = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<string>(nullable: true),
                    Voto = table.Column<int>(nullable: false),
                    DataVoto = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotoFilme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotoFilme_Filme_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotoFilme_IdFilme",
                table: "VotoFilme",
                column: "IdFilme");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotoFilme");
        }
    }
}
