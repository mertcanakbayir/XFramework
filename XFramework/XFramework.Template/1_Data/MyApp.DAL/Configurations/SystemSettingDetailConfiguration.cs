using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;
using MyApp.Helper.Enums;
using MyApp.Helper.Helpers;

namespace MyApp.DAL.Configurations
{
    public class SystemSettingDetailConfiguration : IEntityTypeConfiguration<SystemSettingDetail>
    {
        public void Configure(EntityTypeBuilder<SystemSettingDetail> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Key).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Value).IsRequired();

            builder.Property(s => s.Type)
                 .HasConversion(
                     v => EnumConverter.EnumToChar(v),
                     v => EnumConverter.CharToEnum<SystemSettingType>(v)
                 )
                 .HasMaxLength(6);
        }
    }
}
