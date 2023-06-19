using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace retronatus_backend.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    IdLocal = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.IdLocal);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Is_Super_Admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Publicacao",
                columns: table => new
                {
                    IdPublicacao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdLocal = table.Column<int>(type: "integer", nullable: false),
                    IdCategoria = table.Column<int>(type: "integer", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "integer", nullable: true),
                    LocalIdLocal = table.Column<int>(type: "integer", nullable: true),
                    UsuarioIdUsuario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacao", x => x.IdPublicacao);
                    table.ForeignKey(
                        name: "FK_Publicacao_Categoria_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK_Publicacao_Local_LocalIdLocal",
                        column: x => x.LocalIdLocal,
                        principalTable: "Local",
                        principalColumn: "IdLocal");
                    table.ForeignKey(
                        name: "FK_Publicacao_Usuario_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Comentario",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdPublicacao = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PublicacaoIdPublicacao = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentario", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentario_Publicacao_PublicacaoIdPublicacao",
                        column: x => x.PublicacaoIdPublicacao,
                        principalTable: "Publicacao",
                        principalColumn: "IdPublicacao");
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    IdMedia = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    IdPublicacao = table.Column<int>(type: "integer", nullable: false),
                    PublicacaoIdPublicacao = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.IdMedia);
                    table.ForeignKey(
                        name: "FK_Media_Publicacao_PublicacaoIdPublicacao",
                        column: x => x.PublicacaoIdPublicacao,
                        principalTable: "Publicacao",
                        principalColumn: "IdPublicacao");
                });

            migrationBuilder.CreateTable(
                name: "Resposta",
                columns: table => new
                {
                    IdResposta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdComentario = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ComentarioIdComentario = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resposta", x => x.IdResposta);
                    table.ForeignKey(
                        name: "FK_Resposta_Comentario_ComentarioIdComentario",
                        column: x => x.ComentarioIdComentario,
                        principalTable: "Comentario",
                        principalColumn: "IdComentario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentario_PublicacaoIdPublicacao",
                table: "Comentario",
                column: "PublicacaoIdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Media_PublicacaoIdPublicacao",
                table: "Media",
                column: "PublicacaoIdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacao_CategoriaIdCategoria",
                table: "Publicacao",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacao_LocalIdLocal",
                table: "Publicacao",
                column: "LocalIdLocal");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacao_UsuarioIdUsuario",
                table: "Publicacao",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Resposta_ComentarioIdComentario",
                table: "Resposta",
                column: "ComentarioIdComentario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Resposta");

            migrationBuilder.DropTable(
                name: "Comentario");

            migrationBuilder.DropTable(
                name: "Publicacao");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
