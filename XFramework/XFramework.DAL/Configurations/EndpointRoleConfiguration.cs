using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class EndpointRoleConfiguration : IEntityTypeConfiguration<EndpointRole>
    {
        public void Configure(EntityTypeBuilder<EndpointRole> builder)
        {
            //builder.HasKey(er => er.Id);
            //builder.HasIndex(er => new { er.RoleId, er.EndpointId })
            //    .IsUnique();

            builder.HasKey(er => new {er.RoleId,er.EndpointId });

            builder.HasOne(er => er.Endpoint)
               .WithMany(e => e.EndpointRoles)
               .HasForeignKey(e => e.EndpointId);

            builder.HasOne(er => er.Role)
               .WithMany(r=>r.EndpointRoles)
               .HasForeignKey(er => er.RoleId);
        }
    }
}
