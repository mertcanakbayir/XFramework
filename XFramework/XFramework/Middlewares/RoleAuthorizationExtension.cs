namespace XFramework.API.Middlewares
{
    public static class RoleAuthorizationExtension
    {
        public static IApplicationBuilder UseRoleAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}
