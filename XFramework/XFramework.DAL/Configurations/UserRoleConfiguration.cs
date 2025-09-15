using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //builder.HasKey(ur => ur.Id);
            //builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(r => r.RoleId);
        }
    }
}
