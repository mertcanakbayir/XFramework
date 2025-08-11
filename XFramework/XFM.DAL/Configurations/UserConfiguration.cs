using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFM.DAL.Entities;

namespace XFM.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(120);

            builder.HasOne(u=>u.Role)
                .WithMany(r=>r.Users)
                .HasForeignKey(u=>u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
