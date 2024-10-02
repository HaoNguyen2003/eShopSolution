using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class newcmt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Comments",
               columns: table => new
               {
                   ID = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                   ProductID = table.Column<int>(type: "int", nullable: false),
                   CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                   Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   DisplayCommentLevelID = table.Column<int>(type: "int", nullable: true),
                   ParentCommentID = table.Column<int>(type: "int", nullable: true) 
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Comments", x => x.ID);

                   table.ForeignKey(
                       name: "FK_Comments_AspNetUsers_UserID",
                       column: x => x.UserID,
                       principalTable: "AspNetUsers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);

                   table.ForeignKey(
                       name: "FK_Comments_ParentCommentID",
                       column: x => x.ParentCommentID,
                       principalTable: "Comments",
                       principalColumn: "ID",
                       onDelete: ReferentialAction.Restrict); // Đổi thành Restrict để tránh vòng lặp xoá

                   table.ForeignKey(
                       name: "FK_Comments_Product_ProductID",
                       column: x => x.ProductID,
                       principalTable: "Product",
                       principalColumn: "ID",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentCommentID",
                table: "Comments",
                column: "ParentCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
