using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InfoAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Province",
                table: "AddressShipInfo");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "AddressShipInfo");

            migrationBuilder.RenameColumn(
                name: "district",
                table: "AddressShipInfo",
                newName: "WardCode");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceID",
                table: "AddressShipInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "districtID",
                table: "AddressShipInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvinceID",
                table: "AddressShipInfo");

            migrationBuilder.DropColumn(
                name: "districtID",
                table: "AddressShipInfo");

            migrationBuilder.RenameColumn(
                name: "WardCode",
                table: "AddressShipInfo",
                newName: "district");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "AddressShipInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "AddressShipInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
