﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UserRolePopulated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f", "515155a9-5b64-4957-8ea0-06a84715246c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "45deb9d6-c1ae-44a6-a03b-c9a5cfc15f2f", "515155a9-5b64-4957-8ea0-06a84715246c" });
        }
    }
}
