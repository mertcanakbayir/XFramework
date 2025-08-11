using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r=>r.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasData(
     new Role { Id = 1, Name = "Admin", CreatedAt = new DateTime(2025, 1, 1), IsActive = true },
     new Role { Id = 2, Name = "Moderator", CreatedAt = new DateTime(2025, 1, 1), IsActive = true },
     new Role { Id = 3, Name = "User", CreatedAt = new DateTime(2025, 1, 1), IsActive = true }
 );

        }
    }
}
