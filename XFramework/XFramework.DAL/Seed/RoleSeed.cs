using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1, Name = "Admin", IsActive = true },
                new Role { Id = 2, Name = "Moderator", IsActive = true },
                new Role { Id = 3, Name = "User", IsActive = true }
                );
        }
    }
}
