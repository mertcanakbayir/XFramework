using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Seed
{
    public class PageSeed : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.HasData(
            new Page { Id = 1, PageUrl = "/dashboard", IsActive = true },
            new Page { Id = 2, PageUrl = "/users", IsActive = true }
                );
        }
    }
}
