using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewReferenceTableProductReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_Order_OrderID",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "ProductReview",
                newName: "DetailOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_OrderID",
                table: "ProductReview",
                newName: "IX_ProductReview_DetailOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_DetailOrder_DetailOrderID",
                table: "ProductReview",
                column: "DetailOrderID",
                principalTable: "DetailOrder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReview_DetailOrder_DetailOrderID",
                table: "ProductReview");

            migrationBuilder.RenameColumn(
                name: "DetailOrderID",
                table: "ProductReview",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReview_DetailOrderID",
                table: "ProductReview",
                newName: "IX_ProductReview_OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReview_Order_OrderID",
                table: "ProductReview",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
