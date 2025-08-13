using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Configurations
{
    public class EndpointConfiguration:IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder) {

        }
    }
}
