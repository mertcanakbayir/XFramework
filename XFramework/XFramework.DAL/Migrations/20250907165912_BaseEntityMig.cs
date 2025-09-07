using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PageRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EndpointRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 3, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 4, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 5, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 6, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 11, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 12, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 13, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 2, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 1, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PageRoles",
                keyColumns: new[] { "PageId", "RoleId" },
                keyValues: new object[] { 2, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PageRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EndpointRoles");
        }
    }
}
