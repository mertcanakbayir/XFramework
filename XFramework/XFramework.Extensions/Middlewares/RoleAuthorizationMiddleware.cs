using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using XFramework.BLL.Services.Concretes;

namespace XFramework.Extensions.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RoleAuthorizationService authorizationService)
        {
            var path = context.Request.Path.Value?.ToLower();
            if (path.Contains("/auth/login") || path.Contains("/auth/register")
                || path.Contains("/auth/forgot-password") || path.Contains("/auth/reset-password") || path.Contains("/api/test"))
            {
                await _next(context);
                return;
            }
            if (context.User.Identity?.IsAuthenticated == false)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }


            var endpoint = context.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (actionDescriptor == null)
            {
                await _next(context);
                return;
            }

            string controllerName = actionDescriptor.ControllerName;
            string actionName = actionDescriptor.ActionName;
            string httpMethod = context.Request.Method;
            var userRoles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            bool hasAccess = await authorizationService.HasAccessAsync(controllerName, actionName, httpMethod, userId);

            if (!hasAccess)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden");
                return;
            }
            await _next(context);
        }
    }
}
