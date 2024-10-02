using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NameTableAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AspNetUsers_UserID",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Address_AddressID",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "AddressShipInfo");

            migrationBuilder.RenameIndex(
                name: "IX_Address_UserID",
                table: "AddressShipInfo",
                newName: "IX_AddressShipInfo_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddressShipInfo",
                table: "AddressShipInfo",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressShipInfo_AspNetUsers_UserID",
                table: "AddressShipInfo",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AddressShipInfo_AddressID",
                table: "Order",
                column: "AddressID",
                principalTable: "AddressShipInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressShipInfo_AspNetUsers_UserID",
                table: "AddressShipInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AddressShipInfo_AddressID",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddressShipInfo",
                table: "AddressShipInfo");

            migrationBuilder.RenameTable(
                name: "AddressShipInfo",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_AddressShipInfo_UserID",
                table: "Address",
                newName: "IX_Address_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AspNetUsers_UserID",
                table: "Address",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Address_AddressID",
                table: "Order",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
