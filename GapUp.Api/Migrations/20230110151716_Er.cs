using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GapUp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Er : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateUserId",
                table: "Products",
                newName: "UpdatedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "Products",
                newName: "UpdateUserId");
        }
    }
}
