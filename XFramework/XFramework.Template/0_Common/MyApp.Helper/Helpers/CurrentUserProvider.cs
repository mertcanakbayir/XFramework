using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyApp.Helper.Helpers
{
    public class CurrentUserProvider
    {
        private readonly IHttpContextAccessor _http;
        public CurrentUserProvider(IHttpContextAccessor http)
        {
            _http = http;
        }

        public int GetUserId()
        {
            var userId = _http.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var id))
            {
                return id;
            }
            return 0;
        }
    }
}
