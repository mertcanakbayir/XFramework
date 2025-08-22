using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Endpoints",
                columns: new[] { "Id", "Action", "Controller", "CreatedAt", "CreatedBy", "HttpMethod", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 7, "AddPage", "Page", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 8, "AddEndpoint", "Endpoint", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
