using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class auditmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "SystemSettingDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "SystemSetting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "PageRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Endpoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "EndpointRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "SystemSettingDetail");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "SystemSetting");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "EndpointRoles");
        }
    }
}
