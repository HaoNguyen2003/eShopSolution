using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UniqueKey_Table_CategoryAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryAndBrand_BrandID",
                table: "CategoryAndBrand");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAndBrand_BrandID_CategoryID",
                table: "CategoryAndBrand",
                columns: new[] { "BrandID", "CategoryID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoryAndBrand_BrandID_CategoryID",
                table: "CategoryAndBrand");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAndBrand_BrandID",
                table: "CategoryAndBrand",
                column: "BrandID");
        }
    }
}
