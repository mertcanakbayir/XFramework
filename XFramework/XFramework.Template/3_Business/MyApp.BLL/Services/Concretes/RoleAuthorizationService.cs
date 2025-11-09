using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class RoleAuthorizationService : IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        public RoleAuthorizationService(IMemoryCache memoryCache, IBaseRepository<User> userRepository, IConfiguration configuration)
        {

            _memoryCache = memoryCache;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<List<string>> GetAllPagesByUser(int userId)
        {
            var config = _configuration.GetSection("Cache");
            var userPagecacheMinutes = config.GetValue<int>("UserPageCacheMinutes");
            string PagesCacheKey = "user_pages";
            string cacheKey = $"{PagesCacheKey}:{userId}";
            if (_memoryCache.TryGetValue(cacheKey, out List<string> cachedPages))
            {
                return cachedPages;
            }
            var user = await _userRepository.GetAsync(filter: e => e.Id == userId, include: query => query.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).ThenInclude(pr => pr.PageRoles).ThenInclude(p => p.Page));
            if (user == null)
            {
                return new List<string>();
            }
            int cacheMinutes = _configuration.GetValue<int>("Cache:UserPageCacheMinutes", 10);
            var userPages = user.UserRoles.SelectMany(ur => ur.Role.PageRoles)
                .Select(pr => pr.Page.PageUrl.ToLower())
                .Distinct()
                .ToList();
            _memoryCache.Set(cacheKey, userPages, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(userPagecacheMinutes)
            });
            return userPages;
        }

        private async Task<List<string>> GetAllEndpointsByUser(int userId)
        {
            var config = _configuration.GetSection("Cache");
            var userEndpointCacheMinutes = config.GetValue<int>("UserEndpointCacheMinutes");

            string EndpointsCacheKey = "user_endpoints";
            string cacheKey = $"{EndpointsCacheKey}:{userId}";
            if (_memoryCache.TryGetValue(cacheKey, out List<string> cachedEndpoints))
            {
                return cachedEndpoints;
            }
            var user = await _userRepository.GetAsync(filter: u => u.Id == userId, include: query => query.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).ThenInclude(er => er.EndpointRoles).ThenInclude(e => e.Endpoint));

            if (user == null) { return new List<string>(); }

            var userEndpoints = user.UserRoles.SelectMany(ur => ur.Role.EndpointRoles)
                .Select(er => $"{er.Endpoint.Controller.ToLower()}:{er.Endpoint.Action.ToLower()}:{er.Endpoint.HttpMethod.ToUpper()}").Distinct().ToList();

            _memoryCache.Set(cacheKey, userEndpoints, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(userEndpointCacheMinutes)
            });

            return userEndpoints;
        }


        public async Task<bool> HasAccessAsync(string controller, string action, string httpMethod, int userId)
        {

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
