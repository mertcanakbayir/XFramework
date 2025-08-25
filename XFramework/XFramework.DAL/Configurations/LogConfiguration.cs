using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Message)
               .IsRequired()
               .HasMaxLength(2000);

            builder.Property(l => l.Level)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.Timestamp)
                .IsRequired();

            builder.Property(l => l.Exception)
                .HasMaxLength(4000);

            builder.Property(l => l.ActionName)
                .HasMaxLength(250);

            builder.Property(l => l.IpAddress)
                .HasMaxLength(50);

            builder.Property(l => l.UserId)
                .IsRequired(false);
        }
    }
}
