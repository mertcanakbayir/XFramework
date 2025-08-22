using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SystemSettingDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "SystemSettingDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "SystemSetting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "SystemSetting",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Pages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PageRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PageRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PageRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PageRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PageRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "PageRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Endpoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Endpoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EndpointRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EndpointRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EndpointRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EndpointRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EndpointRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "EndpointRoles",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemSettingDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemSettingDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SystemSetting");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SystemSetting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Endpoints");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EndpointRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EndpointRoles");
        }
    }
}
