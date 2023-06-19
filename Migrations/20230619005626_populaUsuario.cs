using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retronatus_backend.Migrations
{
    /// <inheritdoc />
    public partial class populaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "idusuario", "name", "email", "password", "is_super_admin" },
                values: new object[,]
                {
                    {
                        1,
                        "admin",
                        "admin@admin.com",
                        "GjgU0IFUNCy2uQ+YfmGr5iApI63A7ofHESn9O4COFs4=",
                        true
                    },
                    {
                        2,
                        "user",
                        "user@user.com",
                        "GjgU0IFUNCy2uQ+YfmGr5iApI63A7ofHESn9O4COFs4=",
                        false
                    },
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "usuario",
                keyColumn: "idusuario",
                keyValues: new object[] { 1, 2 }
            );
        }
    }
}
