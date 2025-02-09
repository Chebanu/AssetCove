using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetCove.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsernameNameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Portfolios",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Portfolios",
                newName: "UserId");
        }
    }
}
