using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;
using MyApp.Helper.Enums;

namespace MyApp.DAL.Seed
{
    public class SystemSettingDetailSeed : IEntityTypeConfiguration<SystemSettingDetail>
    {
        public void Configure(EntityTypeBuilder<SystemSettingDetail> builder)
        {
            builder.HasData(
                   new SystemSettingDetail
                   {
                       Id = 1,
                       SystemSettingId = 1,
                       Key = "Ayar1Detail",
                       Value = "Merhaba",
                       Type = SystemSettingType.String,
                       IsActive = true
                   },
                new SystemSettingDetail
                {
                    Id = 2,
                    SystemSettingId = 1,
                    Key = "Ayar2Detail",
                    Value = "2025-08-15 10:30:00",
                    Type = SystemSettingType.DateTime,
                    IsActive = true
                }, new SystemSettingDetail
                {
                    Id = 3,
                    SystemSettingId = 3,
                    Key = "SmtpHost",
                    Value = "smtp.freesmtpservers.com",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 4,
                    SystemSettingId = 3,
                    Key = "SmtpPort",
                    Value = "25",
                    Type = SystemSettingType.Int,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 5,
                    SystemSettingId = 3,
                    Key = "SmtpUser",
                    Value = "",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 6,
                    SystemSettingId = 3,
                    Key = "EncryptedPassword",
                    Value = "",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 7,
                    SystemSettingId = 3,
                    Key = "EnableSsl",
                    Value = "false",
                    Type = SystemSettingType.Bool,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 8,
                    SystemSettingId = 3,
                    Key = "SenderEmail",
                    Value = "deneme@mertcan.com",
                    Type = SystemSettingType.String,
                    IsActive = true
                },
                new SystemSettingDetail
                {
                    Id = 9,
                    SystemSettingId = 3,
                    Key = "IsQueue",
                    Value = "false",
                    IsActive = true
                },
                new SystemSettingDetail
                {
                    Id = 10,
                    SystemSettingId = 4,
                    Key = "IsEnabled",
                    Type = SystemSettingType.Bool,
                    Value = "true",
                    IsActive = true
                }
                );
        }
    }
}
