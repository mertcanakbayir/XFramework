using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    public class UserRoleSeed : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                 new UserRole
                 {
                     Id = 1,
                     RoleId = 1,
                     UserId = 1,
                 }
                );
        }
    }
}
