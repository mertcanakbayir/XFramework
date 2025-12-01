using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    public class EndpointRoleSeed : IEntityTypeConfiguration<EndpointRole>
    {
        public void Configure(EntityTypeBuilder<EndpointRole> builder)
        {
            builder.HasData(
                 new EndpointRole { Id = 1, EndpointId = 1, RoleId = 1 },

                 new EndpointRole { Id = 28, EndpointId = 2, RoleId = 1 },
                 new EndpointRole { Id = 29, EndpointId = 3, RoleId = 1 },
                 new EndpointRole { Id = 30, EndpointId = 4, RoleId = 1 },
                 new EndpointRole { Id = 31, EndpointId = 5, RoleId = 1 },

                 new EndpointRole { Id = 2, EndpointId = 6, RoleId = 1 },
                 new EndpointRole { Id = 3, EndpointId = 7, RoleId = 1 },
                 new EndpointRole { Id = 4, EndpointId = 8, RoleId = 1 },
                 new EndpointRole { Id = 5, EndpointId = 9, RoleId = 1 },
                 new EndpointRole { Id = 6, EndpointId = 10, RoleId = 1 },

                 new EndpointRole { Id = 7, EndpointId = 16, RoleId = 1 },
                 new EndpointRole { Id = 8, EndpointId = 17, RoleId = 1 },

                 new EndpointRole { Id = 9, EndpointId = 23, RoleId = 1 },
                 new EndpointRole { Id = 10, EndpointId = 24, RoleId = 1 },

                 new EndpointRole { Id = 12, EndpointId = 31, RoleId = 1 },
                 new EndpointRole { Id = 13, EndpointId = 32, RoleId = 1 },
                 new EndpointRole { Id = 14, EndpointId = 33, RoleId = 1 },

                 new EndpointRole { Id = 15, EndpointId = 39, RoleId = 1 },
                 new EndpointRole { Id = 16, EndpointId = 40, RoleId = 1 },
                 new EndpointRole { Id = 17, EndpointId = 41, RoleId = 1 },

                 new EndpointRole { Id = 18, EndpointId = 46, RoleId = 1 },
                 new EndpointRole { Id = 19, EndpointId = 47, RoleId = 1 },

                 new EndpointRole { Id = 20, EndpointId = 53, RoleId = 1 },
                 new EndpointRole { Id = 21, EndpointId = 54, RoleId = 1 },
                 new EndpointRole { Id = 22, EndpointId = 55, RoleId = 1 },
                 new EndpointRole { Id = 23, EndpointId = 56, RoleId = 1 },

                 new EndpointRole { Id = 24, EndpointId = 62, RoleId = 1 },
                 new EndpointRole { Id = 25, EndpointId = 63, RoleId = 1 },
                 new EndpointRole { Id = 26, EndpointId = 64, RoleId = 1 },
                 new EndpointRole { Id = 27, EndpointId = 65, RoleId = 1 },

                 new EndpointRole { Id = 32, EndpointId = 42, RoleId = 1 },
                 new EndpointRole { Id = 33, EndpointId = 48, RoleId = 1 }

                );
        }
    }
}
