using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Message)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.MessageTemplate)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.Level)
                .HasMaxLength(128);

            builder.Property(l => l.TimeStamp)
                .IsRequired();

            builder.Property(l => l.Exception)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.Properties)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.UserId)
                .HasMaxLength(100);

            builder.Property(l => l.IPAddress)
                .HasMaxLength(50);

            builder.Property(l => l.ActionName)
                .HasMaxLength(250);

            builder.Property(l => l.TraceIdentifier)
                .HasMaxLength(100);
        }
    }
}
