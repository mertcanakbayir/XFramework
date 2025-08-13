using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Endpoints",
                columns: new[] { "Id", "Action", "Controller", "CreatedAt", "HttpMethod", "IsActive", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "GetAll", "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GET", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Get", "Page", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GET", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Create", "Page", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Page",
                columns: new[] { "Id", "CreatedAt", "IsActive", "PageUrl", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "/dashboard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "/users", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "EndpointRoles",
                columns: new[] { "EndpointId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "PageRoles",
                columns: new[] { "PageId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Page",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
