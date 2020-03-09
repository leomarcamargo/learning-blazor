using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoBlazor.Server.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: false),
                    Resumo = table.Column<string>(nullable: true),
                    EmCartaz = table.Column<bool>(nullable: false),
                    Trailer = table.Column<string>(nullable: true),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    Poster = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Biografia = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneroFilme",
                columns: table => new
                {
                    IdGenero = table.Column<int>(nullable: false),
                    IdFilme = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroFilme", x => new { x.IdGenero, x.IdFilme });
                    table.ForeignKey(
                        name: "FK_GeneroFilme_Filme_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroFilme_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmePessoa",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    IdFilme = table.Column<int>(nullable: false),
                    Personagem = table.Column<string>(nullable: true),
                    Ordem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmePessoa", x => new { x.IdPessoa, x.IdFilme });
                    table.ForeignKey(
                        name: "FK_FilmePessoa_Filme_IdFilme",
                        column: x => x.IdFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmePessoa_Pessoa_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmePessoa_IdFilme",
                table: "FilmePessoa",
                column: "IdFilme");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroFilme_IdFilme",
                table: "GeneroFilme",
                column: "IdFilme");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmePessoa");

            migrationBuilder.DropTable(
                name: "GeneroFilme");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Genero");
        }
    }
}
