using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class isUiqueKeyMenuPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MenuPermission_MenuID",
                table: "MenuPermission");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_MenuID_PermissionID",
                table: "MenuPermission",
                columns: new[] { "MenuID", "PermissionID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MenuPermission_MenuID_PermissionID",
                table: "MenuPermission");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_MenuID",
                table: "MenuPermission",
                column: "MenuID");
        }
    }
}
