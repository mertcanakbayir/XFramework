using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Controllers;
using XFramework.BLL.Services.RoleAuthorizationService;

namespace XFramework.API.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,RoleAuthorizationService authorizationService)
        {

            try
            {
                var path = context.Request.Path.Value?.ToLower();
                if (path.Contains("/auth/login") || path.Contains("/auth/register"))
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
                string pageUrl = context.Request.Headers["PageUrl"].ToString()?.ToLower();
                if (string.IsNullOrEmpty(pageUrl))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Page URL header missing");
                    return;
                }
                bool hasAccess = await authorizationService.HasAccessAsync(pageUrl, controllerName, actionName, httpMethod, userRoles);

                if (!hasAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
            }
        
        }
    }
}
