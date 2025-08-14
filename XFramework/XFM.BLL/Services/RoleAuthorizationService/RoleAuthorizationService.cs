    using Microsoft.EntityFrameworkCore;
    using XFM.DAL;
using XFM.DAL.Abstract;
using XFramework.DAL.Entities;

namespace XFramework.BLL.Services.RoleAuthorizationService
    {
        public class RoleAuthorizationService
        {
        private readonly IBaseRepository<Page> _pageRepository;
        private readonly IBaseRepository<Endpoint> _endpointRepository;
        
            public RoleAuthorizationService(IBaseRepository<Page> pageRepository, IBaseRepository<Endpoint> endpointRepository)
            {
             _pageRepository = pageRepository;
            _endpointRepository = endpointRepository;
            }
            public async Task<bool> HasAccessAsync(string path,string controller,string action, string httpMethod, List<string> userRoles)
            {
            var page = await _pageRepository.GetAsync(e => e.PageUrl.ToLower() == path.ToLower(),
                includeFunc:query=>query.Include(e=>e.PageRoles).ThenInclude(pr=>pr.Role));
            if (page==null)
                {
                    return false;
                }
            var allowedPageRoles=page.PageRoles.Select(pr=>pr.Role.Name).ToList();
            if (!userRoles.Any(r=>allowedPageRoles.Contains(r))) {
                return false;
            }
            var endpoint = await _endpointRepository.GetAsync(filter:e=>e.Controller.ToLower()==controller.ToLower()
                                                                         && e.Action.ToLower()==action.ToLower()
                                                                         && e.HttpMethod.ToUpper()==httpMethod.ToUpper(),
                                                                         includeFunc:query=>query.Include(e=>e.EndpointRoles).ThenInclude(er=>er.Role));
            if (endpoint == null)
            {
                return false;
            }
            var allowedRoles=endpoint.EndpointRoles.Select(er=>er.Role.Name).ToList();
            return userRoles.Any(r => allowedRoles.Contains(r));
            }
        }
    }
