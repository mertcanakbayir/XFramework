using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(120);
        }
    }
}
