using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    public class SystemSettingSeed : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.HasData(
                 new SystemSetting
                 {
                     Id = 1,
                     Name = "Ayar 1",
                     Description = "Ayar denemesi için açıklama 1",
                     IsActive = true
                 },
                new SystemSetting
                {
                    Id = 2,
                    Name = "Ayar 2",
                    Description = "Ayar denemesi için açıklama 2",
                    IsActive = true
                },
                new SystemSetting
                {
                    Id = 3,
                    Name = "Mail Settings",
                    Description = "SMTP Mail Settings",
                    IsActive = true
                },
                new SystemSetting
                {
                    Id = 4,
                    Name = "Log Settings",
                    Description = "System Log Settings"
                }
                );
        }
    }
}
