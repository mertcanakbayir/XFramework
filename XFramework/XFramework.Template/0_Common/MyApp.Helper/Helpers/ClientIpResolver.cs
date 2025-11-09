using Microsoft.AspNetCore.Http;

namespace MyApp.Helper.Helpers
{
    public class ClientIpResolver
    {
        public string GetClientIp(HttpContext context)
        {
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                var ip = forwardedFor.Split(',').FirstOrDefault()?.Trim();
                if (!string.IsNullOrWhiteSpace(ip))
                    return ip;
            }
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown-ip";
        }
    }
}
