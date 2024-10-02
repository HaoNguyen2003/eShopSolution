using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateIDForCategoryAndBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryAndBrand",
                table: "CategoryAndBrand");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "CategoryAndBrand",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryAndBrand",
                table: "CategoryAndBrand",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAndBrand_BrandID",
                table: "CategoryAndBrand",
                column: "BrandID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryAndBrand",
                table: "CategoryAndBrand");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAndBrand_BrandID",
                table: "CategoryAndBrand");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "CategoryAndBrand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryAndBrand",
                table: "CategoryAndBrand",
                columns: new[] { "BrandID", "CategoryID" });
        }
    }
}
