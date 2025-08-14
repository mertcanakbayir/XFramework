using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    internal class MailSettingConfiguration : IEntityTypeConfiguration<MailSetting>
    {
        public void Configure(EntityTypeBuilder<MailSetting> builder)
        {
            builder.HasKey(ms => ms.Id);

            // Property configuration
            builder.Property(ms => ms.SmtpHost)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(ms => ms.SmtpPort)
                .IsRequired();

            builder.Property(ms => ms.SmtpUser)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ms => ms.EncryptedPassword)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(ms => ms.EnableSsl)
                .IsRequired();

            builder.Property(ms => ms.SenderEmail)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
