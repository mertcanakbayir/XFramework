using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class PageRoleConfiguration : IEntityTypeConfiguration<PageRole>
    {
        public void Configure(EntityTypeBuilder<PageRole> builder)
        {
            //builder.HasKey(pr => pr.Id);
            //builder.HasIndex(pr => new { pr.PageId, pr.RoleId }).IsUnique();
            builder.HasKey(pr => new { pr.PageId, pr.RoleId });

            builder.HasOne(pr => pr.Page)
                .WithMany(p => p.PageRoles)
                .HasForeignKey(pr => pr.PageId);

            builder.HasOne(pr => pr.Role)
                .WithMany(r => r.PageRoles)
               .HasForeignKey(pr => pr.RoleId);
        }
    }
}
