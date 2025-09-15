using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class RoleAuthorizationService : IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMemoryCache _memoryCache;
        public RoleAuthorizationService(IMemoryCache memoryCache, IBaseRepository<User> userRepository)
        {

            _memoryCache = memoryCache;
            _userRepository = userRepository;
        }

        private async Task<List<string>> GetAllPagesByUser(int userId)
        {
            string PagesCacheKey = "user_pages";
            string cacheKey = $"{PagesCacheKey}:{userId}";
            if (_memoryCache.TryGetValue(cacheKey, out List<string> cachedPages))
            {
                return cachedPages;
            }
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = e => e.Id == userId,
                IncludeFunc = query => query.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).ThenInclude(pr => pr.PageRoles).ThenInclude(p => p.Page)
            });
            if (user == null)
            {
                return new List<string>();
            }
            var userPages = user.UserRoles.SelectMany(ur => ur.Role.PageRoles)
                .Select(pr => pr.Page.PageUrl.ToLower())
                .Distinct()
                .ToList();
            _memoryCache.Set(cacheKey, userPages, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            return userPages;
        }

        private async Task<List<string>> GetAllEndpointsByUser(int userId)
        {
            string EndpointsCacheKey = "user_endpoints";
            string cacheKey = $"{EndpointsCacheKey}:{userId}";
            if (_memoryCache.TryGetValue(cacheKey, out List<string> cachedEndpoints))
            {
                return cachedEndpoints;
            }
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = u => u.Id == userId,
                IncludeFunc = query => query.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).ThenInclude(er => er.EndpointRoles).ThenInclude(e => e.Endpoint)

            });
            if (user == null) { return new List<string>(); }

            var userEndpoints = user.UserRoles.SelectMany(ur => ur.Role.EndpointRoles)
                .Select(er => $"{er.Endpoint.Controller.ToLower()}:{er.Endpoint.Action.ToLower()}:{er.Endpoint.HttpMethod.ToUpper()}").Distinct().ToList();

            _memoryCache.Set(cacheKey, userEndpoints, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });

            return userEndpoints;
        }


        public async Task<bool> HasAccessAsync(string path, string controller, string action, string httpMethod, int userId)
        {
            var userPages = await GetAllPagesByUser(userId);
            if (!userPages.Contains(path.ToLower()))
            {
                return false;
            }

            var userEndpoints = await GetAllEndpointsByUser(userId);
            string endpointKey = $"{controller.ToLower()}:{action.ToLower()}:{httpMethod.ToUpper()}";

            return userEndpoints.Contains(endpointKey);
        }
        public void ClearUserEndpointCache(int userId)
        {
            string cacheKey = $"user_endpoints:{userId}";
            _memoryCache.Remove(cacheKey);
        }

        public void ClearUserPageCache(int userId)
        {
            string cacheKey = $"user_pages:{userId}";
            _memoryCache.Remove(cacheKey);
        }
    }
}
