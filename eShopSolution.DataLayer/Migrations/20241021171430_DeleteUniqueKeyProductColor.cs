using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUniqueKeyProductColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductID_ColorCombinationID",
                table: "ProductColors");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID_ColorCombinationID",
                table: "ProductColors",
                columns: new[] { "ProductID", "ColorCombinationID" },
                unique: true);
        }
    }
}
