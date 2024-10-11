using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class uniquekeyRoleMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID",
                table: "AspNetRoleMenu");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID_RoleID",
                table: "AspNetRoleMenu",
                columns: new[] { "MenuPermissionID", "RoleID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID_RoleID",
                table: "AspNetRoleMenu");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID",
                table: "AspNetRoleMenu",
                column: "MenuPermissionID");
        }
    }
}
