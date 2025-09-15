using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Seed
{
    internal class EndpointSeed : IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            builder.HasData(
                new Endpoint { Id = 1, Controller = "User", Action = "GetAllUsers", HttpMethod = "GET", IsActive = true },

                new Endpoint { Id = 6, Controller = "Page", Action = "GetPagesByUser", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 7, Controller = "Page", Action = "AddPage", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 8, Controller = "Page", Action = "GetByParentId", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 9, Controller = "Page", Action = "GetPages", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 10, Controller = "Page", Action = "UpdatePage", HttpMethod = "PUT", IsActive = true },

                new Endpoint { Id = 16, Controller = "Endpoint", Action = "AddEndpoint", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 17, Controller = "Endpoint", Action = "GetEndpointsByUser", HttpMethod = "GET", IsActive = true },

                new Endpoint { Id = 23, Controller = "Role", Action = "AddUserRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 24, Controller = "Role", Action = "AddPageRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 25, Controller = "Role", Action = "AddEndpointRole", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 31, Controller = "Mail", Action = "SendMail", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 32, Controller = "Test", Action = "GetEnumTests", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 33, Controller = "Mail", Action = "SendMailMQ", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 39, Controller = "SystemSetting", Action = "SystemSettings", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 40, Controller = "SystemSetting", Action = "GetSystemSettingById", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 41, Controller = "SystemSetting", Action = "UpdateSystemSetting", HttpMethod = "PUT", IsActive = true },


                new Endpoint { Id = 46, Controller = "SystemSettingDetail", Action = "GetSystemSettingDetailById", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 47, Controller = "SystemSettingDetail", Action = "UpdateSystemSettingDetail", HttpMethod = "PUT", IsActive = true }
                );
        }
    }
}
