namespace XFramework.API.Services
{
    public class ClientIpResolver
    {
        public string GetClientIp(HttpContext context)
        {
            // proxy,load balancer varsa ip x-forwarded-for header'ı üzerinden gelir önce onu kontrol
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            // xforwarded varsa gelen ilk değer ip'dir 
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                var ip = forwardedFor.Split(',').FirstOrDefault()?.Trim();
                if (!string.IsNullOrWhiteSpace(ip))
                    return ip;
            }
            // x-forwarded üzerinden gelmiyorsa direkt context üzerinden ip al, ip hiç yoksa unknown yaz.
            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown-ip";
        }
    }
}
