using XFramework.API.Middlewares;

namespace XFramework.API.Extensions
{
    public static class RoleAuthorizationExtension
    {
        public static IApplicationBuilder UseRoleAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}
