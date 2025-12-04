using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;
using XFramework.Helper.Enums;

namespace XFramework.DAL.Seed
{
    public class SystemSettingDetailSeed : IEntityTypeConfiguration<SystemSettingDetail>
    {
        public void Configure(EntityTypeBuilder<SystemSettingDetail> builder)
        {
            builder.HasData(
                new SystemSettingDetail
                {
                    Id = 3,
                    SystemSettingId = 3,
                    Key = "SmtpHost",
                    Value = "<your.smtp.server>",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 4,
                    SystemSettingId = 3,
                    Key = "SmtpPort",
                    Value = "<your.smtp.port>",
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
                    Key = "SmtpPassword",
                    Value = "",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 8,
                    SystemSettingId = 3,
                    Key = "SenderEmail",
                    Value = "<your.sender.email>",
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
