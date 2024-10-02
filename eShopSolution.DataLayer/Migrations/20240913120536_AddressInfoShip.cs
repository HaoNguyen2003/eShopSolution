using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddressInfoShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "district");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Address",
                newName: "Province");

            migrationBuilder.AddColumn<string>(
                name: "AddressInfo",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressInfo",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "district",
                table: "Address",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Address",
                newName: "City");
        }
    }
}
