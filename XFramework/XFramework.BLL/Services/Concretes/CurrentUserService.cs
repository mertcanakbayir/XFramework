using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace XFramework.BLL.Services.Concretes
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var id))
            {
                return id;
            }
            return 0;
        }
    }
}
