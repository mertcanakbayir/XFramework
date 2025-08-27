using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class logSettingMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 3, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 4, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 5, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 6, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 12, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 13, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 2 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 11,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 12,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 13,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 3 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 0);

            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsActive", "Name", "Revision", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Sistem Log Ayarları", true, "Log Ayarları", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 4,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 5,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 6,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 7,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 8,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 9,
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 0);

            migrationBuilder.InsertData(
                table: "SystemSettingDetail",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "Revision", "SystemSettingId", "Type", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[] { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "IsEnabled", 0, 4, "B", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "true" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 3, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 4, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 5, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 6, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 12, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 13, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 2 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 4,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 5,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 6,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 7,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 8,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 11,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 12,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 13,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 3 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 3,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 4,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 5,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 6,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 7,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 8,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 9,
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "Revision",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Revision",
                value: 1);
        }
    }
}
