using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Configurations
{
    public class EndpointConfiguration : IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.HttpMethod).IsRequired();
            builder.Property(x => x.Controller).IsRequired();
            builder.Property(x => x.Action).IsRequired();
        }
    }
}
