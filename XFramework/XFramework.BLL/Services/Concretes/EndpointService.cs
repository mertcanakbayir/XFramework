using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;
using XFramework.Dtos.Endpoint;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class EndpointService : BaseService<Endpoint, EndpointDto, EndpointAddDto, EndpontUpdateDto>
    {
        private readonly IBaseRepository<User> _userRepository;
        public EndpointService(IValidator<EndpointAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Endpoint> baseRepository, IUnitOfWork unitOfWork, IValidator<EndpontUpdateDto> updateDtoValidator, IBaseRepository<User> userRepository) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<List<EndpointDto>>> GetEndpointsByUser(int userId)
        {

            var userEndpoints = await _userRepository.GetAsync(new Repository.Options.BaseRepoOptions<User>
            {
                Filter = f => f.Id == userId,
                IncludeFunc = i => i.Include(e => e.UserRoles).ThenInclude(f => f.Role).ThenInclude(g => g.EndpointRoles).ThenInclude(h => h.Endpoint)
            });
            if (userEndpoints == null)
            {
                return ResultViewModel<List<EndpointDto>>.Failure("Kullanıcıya henüz endpoint yetkisi atanmamış", statusCode: 400);
            }
            var endpoints = userEndpoints.UserRoles.Select(ur => ur.Role).SelectMany(er => er.EndpointRoles).Select(p => p.Endpoint);
            var endpointsDto = _mapper.Map<List<EndpointDto>>(endpoints);
            return ResultViewModel<List<EndpointDto>>.Success(endpointsDto, "Kullanıcının yetkili olduğu endpointler:", 200);
        }
    }
}
