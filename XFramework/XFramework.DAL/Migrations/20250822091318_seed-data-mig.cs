using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seeddatamig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Endpoints",
                columns: new[] { "Id", "Action", "Controller", "CreatedAt", "CreatedBy", "HttpMethod", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "GetAll", "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "GET", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, "Get", "Page", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "GET", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, "Create", "Page", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, "AddUserRole", "Role", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, "AddPageRole", "Role", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, "AddEndpointRole", "Role", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 11, "SendMail", "Mail", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 12, "GetEnumTests", "Test", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "GET", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 13, "SendMailMQ", "Mail", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "POST", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "PageUrl", "ParentId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "/dashboard", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "/users", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "Moderator", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsActive", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ayar denemesi için açıklama 1", true, "Ayar 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ayar denemesi için açıklama 2", true, "Ayar 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "SMTP mail gönderim ayarları", true, "Mail Ayarları", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "EndpointRoles",
                columns: new[] { "EndpointId", "RoleId", "CreatedAt", "CreatedBy", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 11, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 12, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 13, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "PageRoles",
                columns: new[] { "PageId", "RoleId", "CreatedAt", "CreatedBy", "IsActive", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "SystemSettingDetail",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "SystemSettingId", "Type", "UpdatedAt", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "Ayar1Detail", 1, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Merhaba" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "Ayar2Detail", 1, "DT", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2025-08-15 10:30:00" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "SmtpHost", 3, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "smtp.freesmtpservers.com" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "SmtpPort", 3, "I", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "25" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "SmtpUser", 3, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "EncryptedPassword", 3, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "EnableSsl", 3, "B", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "false" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "SenderEmail", 3, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "deneme@mertcan.com" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, true, "IsQueue", 3, "S", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "false" }
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
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "EndpointRoles",
                keyColumns: new[] { "EndpointId", "RoleId" },
                keyValues: new object[] { 13, 1 });

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
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SystemSettingDetail",
                keyColumn: "Id",
                keyValue: 9);

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
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Endpoints",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
