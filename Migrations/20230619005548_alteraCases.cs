using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retronatus_backend.Migrations
{
    /// <inheritdoc />
    public partial class alteraCases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Publicacao_PublicacaoIdPublicacao",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Publicacao_PublicacaoIdPublicacao",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicacao_Categoria_CategoriaIdCategoria",
                table: "Publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicacao_Local_LocalIdLocal",
                table: "Publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicacao_Usuario_UsuarioIdUsuario",
                table: "Publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_Comentario_ComentarioIdComentario",
                table: "Resposta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resposta",
                table: "Resposta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publicacao",
                table: "Publicacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Local",
                table: "Local");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "usuario");

            migrationBuilder.RenameTable(
                name: "Resposta",
                newName: "resposta");

            migrationBuilder.RenameTable(
                name: "Publicacao",
                newName: "publicacao");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "media");

            migrationBuilder.RenameTable(
                name: "Local",
                newName: "local");

            migrationBuilder.RenameTable(
                name: "Comentario",
                newName: "comentario");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "categoria");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "usuario",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "usuario",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Is_Super_Admin",
                table: "usuario",
                newName: "is_super_admin");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "usuario",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "usuario",
                newName: "idusuario");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "resposta",
                newName: "idusuario");

            migrationBuilder.RenameColumn(
                name: "IdComentario",
                table: "resposta",
                newName: "idcomentario");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "resposta",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "resposta",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "ComentarioIdComentario",
                table: "resposta",
                newName: "comentarioidcomentario");

            migrationBuilder.RenameColumn(
                name: "IdResposta",
                table: "resposta",
                newName: "idresposta");

            migrationBuilder.RenameIndex(
                name: "IX_Resposta_ComentarioIdComentario",
                table: "resposta",
                newName: "IX_resposta_comentarioidcomentario");

            migrationBuilder.RenameColumn(
                name: "UsuarioIdUsuario",
                table: "publicacao",
                newName: "usuarioidusuario");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "publicacao",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "LocalIdLocal",
                table: "publicacao",
                newName: "localidlocal");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "publicacao",
                newName: "idusuario");

            migrationBuilder.RenameColumn(
                name: "IdLocal",
                table: "publicacao",
                newName: "idlocal");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "publicacao",
                newName: "idcategoria");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "publicacao",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "publicacao",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "CategoriaIdCategoria",
                table: "publicacao",
                newName: "categoriaidcategoria");

            migrationBuilder.RenameColumn(
                name: "IdPublicacao",
                table: "publicacao",
                newName: "idpublicacao");

            migrationBuilder.RenameIndex(
                name: "IX_Publicacao_UsuarioIdUsuario",
                table: "publicacao",
                newName: "IX_publicacao_usuarioidusuario");

            migrationBuilder.RenameIndex(
                name: "IX_Publicacao_LocalIdLocal",
                table: "publicacao",
                newName: "IX_publicacao_localidlocal");

            migrationBuilder.RenameIndex(
                name: "IX_Publicacao_CategoriaIdCategoria",
                table: "publicacao",
                newName: "IX_publicacao_categoriaidcategoria");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "media",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "media",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "PublicacaoIdPublicacao",
                table: "media",
                newName: "publicacaoidpublicacao");

            migrationBuilder.RenameColumn(
                name: "IdPublicacao",
                table: "media",
                newName: "idpublicacao");

            migrationBuilder.RenameColumn(
                name: "IdMedia",
                table: "media",
                newName: "idmedia");

            migrationBuilder.RenameIndex(
                name: "IX_Media_PublicacaoIdPublicacao",
                table: "media",
                newName: "IX_media_publicacaoidpublicacao");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "local",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "local",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "IdLocal",
                table: "local",
                newName: "idlocal");

            migrationBuilder.RenameColumn(
                name: "PublicacaoIdPublicacao",
                table: "comentario",
                newName: "publicacaoidpublicacao");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "comentario",
                newName: "idusuario");

            migrationBuilder.RenameColumn(
                name: "IdPublicacao",
                table: "comentario",
                newName: "idpublicacao");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "comentario",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "comentario",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "IdComentario",
                table: "comentario",
                newName: "idcomentario");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_PublicacaoIdPublicacao",
                table: "comentario",
                newName: "IX_comentario_publicacaoidpublicacao");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categoria",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "categoria",
                newName: "idcategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "idusuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resposta",
                table: "resposta",
                column: "idresposta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_publicacao",
                table: "publicacao",
                column: "idpublicacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_media",
                table: "media",
                column: "idmedia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_local",
                table: "local",
                column: "idlocal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comentario",
                table: "comentario",
                column: "idcomentario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoria",
                table: "categoria",
                column: "idcategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_comentario_publicacao_publicacaoidpublicacao",
                table: "comentario",
                column: "publicacaoidpublicacao",
                principalTable: "publicacao",
                principalColumn: "idpublicacao");

            migrationBuilder.AddForeignKey(
                name: "FK_media_publicacao_publicacaoidpublicacao",
                table: "media",
                column: "publicacaoidpublicacao",
                principalTable: "publicacao",
                principalColumn: "idpublicacao");

            migrationBuilder.AddForeignKey(
                name: "FK_publicacao_categoria_categoriaidcategoria",
                table: "publicacao",
                column: "categoriaidcategoria",
                principalTable: "categoria",
                principalColumn: "idcategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_publicacao_local_localidlocal",
                table: "publicacao",
                column: "localidlocal",
                principalTable: "local",
                principalColumn: "idlocal");

            migrationBuilder.AddForeignKey(
                name: "FK_publicacao_usuario_usuarioidusuario",
                table: "publicacao",
                column: "usuarioidusuario",
                principalTable: "usuario",
                principalColumn: "idusuario");

            migrationBuilder.AddForeignKey(
                name: "FK_resposta_comentario_comentarioidcomentario",
                table: "resposta",
                column: "comentarioidcomentario",
                principalTable: "comentario",
                principalColumn: "idcomentario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentario_publicacao_publicacaoidpublicacao",
                table: "comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_media_publicacao_publicacaoidpublicacao",
                table: "media");

            migrationBuilder.DropForeignKey(
                name: "FK_publicacao_categoria_categoriaidcategoria",
                table: "publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_publicacao_local_localidlocal",
                table: "publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_publicacao_usuario_usuarioidusuario",
                table: "publicacao");

            migrationBuilder.DropForeignKey(
                name: "FK_resposta_comentario_comentarioidcomentario",
                table: "resposta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resposta",
                table: "resposta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_publicacao",
                table: "publicacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_media",
                table: "media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_local",
                table: "local");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comentario",
                table: "comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categoria",
                table: "categoria");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "resposta",
                newName: "Resposta");

            migrationBuilder.RenameTable(
                name: "publicacao",
                newName: "Publicacao");

            migrationBuilder.RenameTable(
                name: "media",
                newName: "Media");

            migrationBuilder.RenameTable(
                name: "local",
                newName: "Local");

            migrationBuilder.RenameTable(
                name: "comentario",
                newName: "Comentario");

            migrationBuilder.RenameTable(
                name: "categoria",
                newName: "Categoria");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Usuario",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Usuario",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "is_super_admin",
                table: "Usuario",
                newName: "Is_Super_Admin");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuario",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "idusuario",
                table: "Usuario",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "idusuario",
                table: "Resposta",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "idcomentario",
                table: "Resposta",
                newName: "IdComentario");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Resposta",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Resposta",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "comentarioidcomentario",
                table: "Resposta",
                newName: "ComentarioIdComentario");

            migrationBuilder.RenameColumn(
                name: "idresposta",
                table: "Resposta",
                newName: "IdResposta");

            migrationBuilder.RenameIndex(
                name: "IX_resposta_comentarioidcomentario",
                table: "Resposta",
                newName: "IX_Resposta_ComentarioIdComentario");

            migrationBuilder.RenameColumn(
                name: "usuarioidusuario",
                table: "Publicacao",
                newName: "UsuarioIdUsuario");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Publicacao",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "localidlocal",
                table: "Publicacao",
                newName: "LocalIdLocal");

            migrationBuilder.RenameColumn(
                name: "idusuario",
                table: "Publicacao",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "idlocal",
                table: "Publicacao",
                newName: "IdLocal");

            migrationBuilder.RenameColumn(
                name: "idcategoria",
                table: "Publicacao",
                newName: "IdCategoria");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Publicacao",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Publicacao",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "categoriaidcategoria",
                table: "Publicacao",
                newName: "CategoriaIdCategoria");

            migrationBuilder.RenameColumn(
                name: "idpublicacao",
                table: "Publicacao",
                newName: "IdPublicacao");

            migrationBuilder.RenameIndex(
                name: "IX_publicacao_usuarioidusuario",
                table: "Publicacao",
                newName: "IX_Publicacao_UsuarioIdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_publicacao_localidlocal",
                table: "Publicacao",
                newName: "IX_Publicacao_LocalIdLocal");

            migrationBuilder.RenameIndex(
                name: "IX_publicacao_categoriaidcategoria",
                table: "Publicacao",
                newName: "IX_Publicacao_CategoriaIdCategoria");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Media",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "Media",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "publicacaoidpublicacao",
                table: "Media",
                newName: "PublicacaoIdPublicacao");

            migrationBuilder.RenameColumn(
                name: "idpublicacao",
                table: "Media",
                newName: "IdPublicacao");

            migrationBuilder.RenameColumn(
                name: "idmedia",
                table: "Media",
                newName: "IdMedia");

            migrationBuilder.RenameIndex(
                name: "IX_media_publicacaoidpublicacao",
                table: "Media",
                newName: "IX_Media_PublicacaoIdPublicacao");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Local",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Local",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "idlocal",
                table: "Local",
                newName: "IdLocal");

            migrationBuilder.RenameColumn(
                name: "publicacaoidpublicacao",
                table: "Comentario",
                newName: "PublicacaoIdPublicacao");

            migrationBuilder.RenameColumn(
                name: "idusuario",
                table: "Comentario",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "idpublicacao",
                table: "Comentario",
                newName: "IdPublicacao");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Comentario",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Comentario",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "idcomentario",
                table: "Comentario",
                newName: "IdComentario");

            migrationBuilder.RenameIndex(
                name: "IX_comentario_publicacaoidpublicacao",
                table: "Comentario",
                newName: "IX_Comentario_PublicacaoIdPublicacao");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categoria",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "idcategoria",
                table: "Categoria",
                newName: "IdCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resposta",
                table: "Resposta",
                column: "IdResposta");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publicacao",
                table: "Publicacao",
                column: "IdPublicacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "IdMedia");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Local",
                table: "Local",
                column: "IdLocal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "IdComentario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Publicacao_PublicacaoIdPublicacao",
                table: "Comentario",
                column: "PublicacaoIdPublicacao",
                principalTable: "Publicacao",
                principalColumn: "IdPublicacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Publicacao_PublicacaoIdPublicacao",
                table: "Media",
                column: "PublicacaoIdPublicacao",
                principalTable: "Publicacao",
                principalColumn: "IdPublicacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacao_Categoria_CategoriaIdCategoria",
                table: "Publicacao",
                column: "CategoriaIdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacao_Local_LocalIdLocal",
                table: "Publicacao",
                column: "LocalIdLocal",
                principalTable: "Local",
                principalColumn: "IdLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicacao_Usuario_UsuarioIdUsuario",
                table: "Publicacao",
                column: "UsuarioIdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_Comentario_ComentarioIdComentario",
                table: "Resposta",
                column: "ComentarioIdComentario",
                principalTable: "Comentario",
                principalColumn: "IdComentario");
        }
    }
}
