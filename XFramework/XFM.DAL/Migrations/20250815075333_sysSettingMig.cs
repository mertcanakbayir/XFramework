using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class sysSettingMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayar denemesi için açıklama 1", true, "Ayar 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ayar denemesi için açıklama 2", true, "Ayar 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SystemSettingDetail",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Key", "SystemSettingId", "Type", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Ayar1Detail", 1, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Merhaba" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Ayar2Detail", 1, "DT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2025-08-15 10:30:00" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
