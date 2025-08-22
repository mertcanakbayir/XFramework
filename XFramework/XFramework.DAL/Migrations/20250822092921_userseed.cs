using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class userseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "IsActive", "Password", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1089), 1, "admin@test.com", true, "AQAAAAIAAYagAAAAEBV9CdhPcsGb7++CWXFC+hFKkqkxKR7LfvNFEgWzEBolNu1bW3WXvLd5FF/mCcDwAw==", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1089), null, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "CreatedAt", "CreatedBy", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
