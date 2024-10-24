using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UniqueKey_Table_ProductSizeInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSizeInventory_SizeID",
                table: "ProductSizeInventory");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeInventory_SizeID_ProductColorID",
                table: "ProductSizeInventory",
                columns: new[] { "SizeID", "ProductColorID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSizeInventory_SizeID_ProductColorID",
                table: "ProductSizeInventory");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizeInventory_SizeID",
                table: "ProductSizeInventory",
                column: "SizeID");
        }
    }
}
