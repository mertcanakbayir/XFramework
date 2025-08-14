using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class SystemSettingDetailConfiguration : IEntityTypeConfiguration<SystemSettingDetail>
    {
        public void Configure(EntityTypeBuilder<SystemSettingDetail> builder)
        {

           builder.HasKey(x=>x.Id);

           builder.Property(x=>x.Key).IsRequired();

            builder.Property(x=>x.Value).IsRequired();

            builder.Property(x=>x.Type).HasMaxLength(25);

        }
    }
}
