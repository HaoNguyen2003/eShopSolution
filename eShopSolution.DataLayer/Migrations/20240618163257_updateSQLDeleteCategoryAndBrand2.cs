using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateSQLDeleteCategoryAndBrand2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAndBrand_Brand_BrandID",
                table: "CategoryAndBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAndBrand_Category_CategoryID",
                table: "CategoryAndBrand");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAndBrand_Brand_BrandID",
                table: "CategoryAndBrand",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAndBrand_Category_CategoryID",
                table: "CategoryAndBrand",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAndBrand_Brand_BrandID",
                table: "CategoryAndBrand");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAndBrand_Category_CategoryID",
                table: "CategoryAndBrand");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAndBrand_Brand_BrandID",
                table: "CategoryAndBrand",
                column: "BrandID",
                principalTable: "Brand",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAndBrand_Category_CategoryID",
                table: "CategoryAndBrand",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
