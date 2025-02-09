using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetCove.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRemovedProperyForAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "UserAssets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Portfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "UserAssets");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Portfolios");
        }
    }
}
