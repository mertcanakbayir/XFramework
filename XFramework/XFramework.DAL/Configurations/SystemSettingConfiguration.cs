using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(2000);

            builder.HasMany(s => s.SystemSettingDetails)  
          .WithOne(d => d.SystemSetting)
          .HasForeignKey(d => d.SystemSettingId)
          .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
