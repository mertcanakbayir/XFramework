using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XFramework.BLL.Result;
using XFramework.DAL.Abstract;
using XFramework.DAL.Entities;
using XFramework.Dtos;

namespace XFramework.BLL.Services.Concretes
{
    public class EndpointService
    {
        private readonly IBaseRepository<Endpoint> _endpointRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public EndpointService(IBaseRepository<Endpoint> endpointRepository, IMapper mapper)
        {
            _endpointRepository = endpointRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel<List<EndpointDto>>> GetEndpointsByUser(int userId)
        {
            var userEndpoints = await _userRepository.GetAsync(f => f.Id == userId,
                includeFunc: i => i.Include(e => e.UserRoles).ThenInclude(f => f.Role).ThenInclude(g => g.EndpointRoles).ThenInclude(h => h.Endpoint));
            if (userEndpoints == null)
            {
                return ResultViewModel<List<EndpointDto>>.Failure("Kullanıcıya henüz endpoint yetkisi atanmamış", statusCode: 400);
            }
            var endpoints = userEndpoints.UserRoles.Select(ur => ur.Role).SelectMany(er => er.EndpointRoles).Select(p => p.Endpoint);
            var endpointsDto = _mapper.Map<List<EndpointDto>>(endpoints);
            return ResultViewModel<List<EndpointDto>>.Success(endpointsDto, "Kullanıcının yetkili olduğu endpointler:", 200);
        }

        public async Task<ResultViewModel<string>> AddEndpoint(EndpointAddDto endpointAddDto)
        {
            var endpointEntity = _mapper.Map<Endpoint>(endpointAddDto);
            await _endpointRepository.AddAsync(endpointEntity);
            return ResultViewModel<string>.Success("Endpoint eklendi", 200);


        }
    }
}
