using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FOREIGNKEYColorCombinationColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorCombinationColor_Colors_ColorCombinationID",
                table: "ColorCombinationColor");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorCombinationColor_Colors_ColorID",
                table: "ColorCombinationColor",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorCombinationColor_Colors_ColorID",
                table: "ColorCombinationColor");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorCombinationColor_Colors_ColorCombinationID",
                table: "ColorCombinationColor",
                column: "ColorCombinationID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
