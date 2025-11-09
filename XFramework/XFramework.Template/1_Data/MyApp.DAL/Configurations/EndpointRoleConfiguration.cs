using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Configurations
{
    public class EndpointRoleConfiguration : IEntityTypeConfiguration<EndpointRole>
    {
        public void Configure(EntityTypeBuilder<EndpointRole> builder)
        {
            builder.HasKey(er => er.Id);
            builder.HasIndex(er => new { er.RoleId, er.EndpointId })
                .IsUnique();

            builder.HasOne(er => er.Endpoint)
               .WithMany(e => e.EndpointRoles)
               .HasForeignKey(e => e.EndpointId);

            builder.HasOne(er => er.Role)
               .WithMany(r => r.EndpointRoles)
               .HasForeignKey(er => er.RoleId);
        }
    }
}
