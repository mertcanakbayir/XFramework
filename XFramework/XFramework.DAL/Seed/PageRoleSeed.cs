using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    public class PageRoleSeed : IEntityTypeConfiguration<PageRole>
    {
        public void Configure(EntityTypeBuilder<PageRole> builder)
        {
            builder.HasData(
                new PageRole { Id = 1, PageId = 1, RoleId = 1 },
                new PageRole { Id = 2, PageId = 2, RoleId = 1 },

                new PageRole { Id = 3, PageId = 1, RoleId = 2 },

                new PageRole { Id = 4, PageId = 1, RoleId = 3 }
                );
        }
    }
}
