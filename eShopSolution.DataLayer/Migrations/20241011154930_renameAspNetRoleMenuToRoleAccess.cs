using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class renameAspNetRoleMenuToRoleAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleMenu");

            migrationBuilder.CreateTable(
                name: "AspNetRoleAccess",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuPermissionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleAccess", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AspNetRoleAccess_AspNetRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleAccess_MenuPermission_MenuPermissionID",
                        column: x => x.MenuPermissionID,
                        principalTable: "MenuPermission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleAccess_MenuPermissionID_RoleID",
                table: "AspNetRoleAccess",
                columns: new[] { "MenuPermissionID", "RoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleAccess_RoleID",
                table: "AspNetRoleAccess",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleAccess");

            migrationBuilder.CreateTable(
                name: "AspNetRoleMenu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuPermissionID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleMenu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AspNetRoleMenu_AspNetRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleMenu_MenuPermission_MenuPermissionID",
                        column: x => x.MenuPermissionID,
                        principalTable: "MenuPermission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_MenuPermissionID_RoleID",
                table: "AspNetRoleMenu",
                columns: new[] { "MenuPermissionID", "RoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_RoleID",
                table: "AspNetRoleMenu",
                column: "RoleID");
        }
    }
}
