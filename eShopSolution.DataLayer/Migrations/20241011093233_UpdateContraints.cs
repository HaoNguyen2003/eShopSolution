using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleMenu_AspNetMenu_MenuID",
                table: "AspNetRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPermission_AspNetRoleMenu_RoleMenuID",
                table: "MenuPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuPermission",
                table: "MenuPermission");

            migrationBuilder.RenameColumn(
                name: "RoleMenuID",
                table: "MenuPermission",
                newName: "MenuID");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                table: "AspNetRoleMenu",
                newName: "MenuPermissionID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleMenu_MenuID",
                table: "AspNetRoleMenu",
                newName: "IX_AspNetRoleMenu_MenuPermissionID");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "MenuPermission",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuPermission",
                table: "MenuPermission",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_MenuID",
                table: "MenuPermission",
                column: "MenuID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleMenu_MenuPermission_MenuPermissionID",
                table: "AspNetRoleMenu",
                column: "MenuPermissionID",
                principalTable: "MenuPermission",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPermission_AspNetMenu_MenuID",
                table: "MenuPermission",
                column: "MenuID",
                principalTable: "AspNetMenu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleMenu_MenuPermission_MenuPermissionID",
                table: "AspNetRoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPermission_AspNetMenu_MenuID",
                table: "MenuPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuPermission",
                table: "MenuPermission");

            migrationBuilder.DropIndex(
                name: "IX_MenuPermission_MenuID",
                table: "MenuPermission");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "MenuPermission");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                table: "MenuPermission",
                newName: "RoleMenuID");

            migrationBuilder.RenameColumn(
                name: "MenuPermissionID",
                table: "AspNetRoleMenu",
                newName: "MenuID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID",
                table: "AspNetRoleMenu",
                newName: "IX_AspNetRoleMenu_MenuID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuPermission",
                table: "MenuPermission",
                columns: new[] { "RoleMenuID", "PermissionID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleMenu_AspNetMenu_MenuID",
                table: "AspNetRoleMenu",
                column: "MenuID",
                principalTable: "AspNetMenu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPermission_AspNetRoleMenu_RoleMenuID",
                table: "MenuPermission",
                column: "RoleMenuID",
                principalTable: "AspNetRoleMenu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
