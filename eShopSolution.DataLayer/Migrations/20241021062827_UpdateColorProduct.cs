using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColorProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "ProductColors",
                newName: "ColorCombinationID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ColorID",
                table: "ProductColors",
                newName: "IX_ProductColors_ColorCombinationID");

            migrationBuilder.CreateTable(
                name: "ColorCombination",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorCombination", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ColorCombinationColor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorCombinationID = table.Column<int>(type: "int", nullable: false),
                    ColorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorCombinationColor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ColorCombinationColor_ColorCombination_ColorCombinationID",
                        column: x => x.ColorCombinationID,
                        principalTable: "ColorCombination",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorCombinationColor_Colors_ColorCombinationID",
                        column: x => x.ColorCombinationID,
                        principalTable: "Colors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID_ColorCombinationID",
                table: "ProductColors",
                columns: new[] { "ProductID", "ColorCombinationID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorCombinationColor_ColorCombinationID",
                table: "ColorCombinationColor",
                column: "ColorCombinationID");

            migrationBuilder.CreateIndex(
                name: "IX_ColorCombinationColor_ColorID_ColorCombinationID",
                table: "ColorCombinationColor",
                columns: new[] { "ColorID", "ColorCombinationID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_ColorCombination_ColorCombinationID",
                table: "ProductColors",
                column: "ColorCombinationID",
                principalTable: "ColorCombination",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_ColorCombination_ColorCombinationID",
                table: "ProductColors");

            migrationBuilder.DropTable(
                name: "ColorCombinationColor");

            migrationBuilder.DropTable(
                name: "ColorCombination");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ProductID_ColorCombinationID",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "ColorCombinationID",
                table: "ProductColors",
                newName: "ColorID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ColorCombinationID",
                table: "ProductColors",
                newName: "IX_ProductColors_ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_ColorID",
                table: "ProductColors",
                column: "ColorID",
                principalTable: "Colors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
