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
                new Endpoint { Id = 2, Controller = "User", Action = "AddUser", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 3, Controller = "User", Action = "UpdateUser", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 4, Controller = "User", Action = "DeleteUserById", HttpMethod = "DELETE", IsActive = true },
                new Endpoint { Id = 5, Controller = "User", Action = "GetUserById", HttpMethod = "GET", IsActive = true },

                new Endpoint { Id = 6, Controller = "Page", Action = "GetPagesByUser", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 7, Controller = "Page", Action = "AddPage", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 8, Controller = "Page", Action = "GetByParentId", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 9, Controller = "Page", Action = "GetPages", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 10, Controller = "Page", Action = "UpdatePage", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 11, Controller = "Page", Action = "DeletePage", HttpMethod = "DELETE", IsActive = true },

                new Endpoint { Id = 16, Controller = "Endpoint", Action = "AddEndpoint", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 17, Controller = "Endpoint", Action = "GetEndpointsByUser", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 18, Controller = "Endpoint", Action = "UpdateEndpoint", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 19, Controller = "Endpoint", Action = "DeleteEndpoint", HttpMethod = "DELETE", IsActive = true },

                new Endpoint { Id = 23, Controller = "Role", Action = "AddRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 24, Controller = "Role", Action = "DeleteRole", HttpMethod = "DELETE", IsActive = true },
                new Endpoint { Id = 25, Controller = "Role", Action = "UpdateRole", HttpMethod = "PUT", IsActive = true },

                new Endpoint { Id = 31, Controller = "Mail", Action = "SendMail", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 32, Controller = "Test", Action = "GetEnumTests", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 33, Controller = "Mail", Action = "SendMailMQ", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 39, Controller = "SystemSetting", Action = "SystemSettings", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 40, Controller = "SystemSetting", Action = "GetSystemSettingById", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 41, Controller = "SystemSetting", Action = "UpdateSystemSetting", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 42, Controller = "SystemSetting", Action = "AddSystemSetting", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 46, Controller = "SystemSettingDetail", Action = "GetSystemSettingDetailById", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 47, Controller = "SystemSettingDetail", Action = "UpdateSystemSettingDetail", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 48, Controller = "SystemSettingDetail", Action = "AddSystemSettingDetail", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 53, Controller = "EndpointRole", Action = "AddEndpointRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 54, Controller = "EndpointRole", Action = "GetEndpointRolesByEndpointId", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 55, Controller = "EndpointRole", Action = "GetEndpointRolesByRoleId", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 56, Controller = "EndpointRole", Action = "UpdateEndpointRole", HttpMethod = "PUT", IsActive = true },

                new Endpoint { Id = 62, Controller = "PageRole", Action = "AddPageRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 63, Controller = "PageRole", Action = "GetPageRolesByPageId", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 64, Controller = "PageRole", Action = "GetPageRolesByRoleId", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 65, Controller = "PageRole", Action = "UpdatePageRole", HttpMethod = "PUT", IsActive = true },
                new Endpoint { Id = 70, Controller = "PageRole", Action = "DeletePageRole", HttpMethod = "DELETE", IsActive = true },

                new Endpoint { Id = 66, Controller = "UserRole", Action = "Assign", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 67, Controller = "UserRole", Action = "GetAssignedRoles", HttpMethod = "GET", IsActive = true }
                );
        }
    }
}
