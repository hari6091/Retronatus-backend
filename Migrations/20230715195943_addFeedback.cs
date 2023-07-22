using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace retronatus_backend.Migrations
{
    /// <inheritdoc />
    public partial class addFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "feedback",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "feedback");
        }
    }
}
