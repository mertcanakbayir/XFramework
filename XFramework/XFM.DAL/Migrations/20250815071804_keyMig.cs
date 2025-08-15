using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XFramework.DAL.Migrations
{
    /// <inheritdoc />
    public partial class keyMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MailSettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "SystemSettingDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Key",
                table: "SystemSettingDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "MailSettings",
                columns: new[] { "Id", "CreatedAt", "EnableSsl", "EncryptedPassword", "IsActive", "SenderEmail", "SmtpHost", "SmtpPort", "SmtpUser", "UpdatedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "ENCRYPTED_PASSWORD_HERE", false, "your-email@example.com", "smtp.example.com", 587, "your-email@example.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
