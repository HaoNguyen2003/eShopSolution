using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class KeyForProductSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrder_productSizeInventories_ProductSizeInventoryID",
                table: "DetailOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_productSizeInventories_ProductColors_ProductColorID",
                table: "productSizeInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_productSizeInventories_Sizes_SizeID",
                table: "productSizeInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productSizeInventories",
                table: "productSizeInventories");

            migrationBuilder.RenameTable(
                name: "productSizeInventories",
                newName: "ProductSizeInventory");

            migrationBuilder.RenameIndex(
                name: "IX_productSizeInventories_SizeID",
                table: "ProductSizeInventory",
                newName: "IX_ProductSizeInventory_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_productSizeInventories_ProductColorID",
                table: "ProductSizeInventory",
                newName: "IX_ProductSizeInventory_ProductColorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSizeInventory",
                table: "ProductSizeInventory",
                column: "ProductSizeInventoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrder_ProductSizeInventory_ProductSizeInventoryID",
                table: "DetailOrder",
                column: "ProductSizeInventoryID",
                principalTable: "ProductSizeInventory",
                principalColumn: "ProductSizeInventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeInventory_ProductColors_ProductColorID",
                table: "ProductSizeInventory",
                column: "ProductColorID",
                principalTable: "ProductColors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizeInventory_Sizes_SizeID",
                table: "ProductSizeInventory",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrder_ProductSizeInventory_ProductSizeInventoryID",
                table: "DetailOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeInventory_ProductColors_ProductColorID",
                table: "ProductSizeInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizeInventory_Sizes_SizeID",
                table: "ProductSizeInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSizeInventory",
                table: "ProductSizeInventory");

            migrationBuilder.RenameTable(
                name: "ProductSizeInventory",
                newName: "productSizeInventories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeInventory_SizeID",
                table: "productSizeInventories",
                newName: "IX_productSizeInventories_SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSizeInventory_ProductColorID",
                table: "productSizeInventories",
                newName: "IX_productSizeInventories_ProductColorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productSizeInventories",
                table: "productSizeInventories",
                column: "ProductSizeInventoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrder_productSizeInventories_ProductSizeInventoryID",
                table: "DetailOrder",
                column: "ProductSizeInventoryID",
                principalTable: "productSizeInventories",
                principalColumn: "ProductSizeInventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productSizeInventories_ProductColors_ProductColorID",
                table: "productSizeInventories",
                column: "ProductColorID",
                principalTable: "ProductColors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productSizeInventories_Sizes_SizeID",
                table: "productSizeInventories",
                column: "SizeID",
                principalTable: "Sizes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
