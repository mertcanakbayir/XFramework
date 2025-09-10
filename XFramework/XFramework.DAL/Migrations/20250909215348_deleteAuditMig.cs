using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class deleteAuditMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SystemSettingDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "SystemSettingDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "SystemSetting",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "SystemSetting",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Pages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PageRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "PageRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Endpoints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Endpoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "EndpointRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "EndpointRoles",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 3, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 4, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 5, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 6, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 12, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 13, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 3 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DeletedAt", "DeletedBy" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SystemSettingDetail");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SystemSettingDetail");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SystemSetting");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SystemSetting");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "EndpointRoles");
        }
    }
}
