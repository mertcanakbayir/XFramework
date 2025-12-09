using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.Endpoint;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class EndpointService : BaseService<Endpoint, EndpointDto, EndpointAddDto, EndpointUpdateDto>, IRegister
    {
        private readonly IBaseRepository<User> _userRepository;
        public EndpointService(IValidator<EndpointAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Endpoint> baseRepository, IUnitOfWork unitOfWork, IValidator<EndpointUpdateDto> updateDtoValidator, IBaseRepository<User> userRepository) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResultViewModel<EndpointDto>> GetEndpointsByUser(int userId)
        {
            var result = await _baseRepository.GetAllAsync<EndpointDto>(
                filter: e => e.EndpointRoles.Any(er => er.Role.UserRoles.Any(ur => ur.UserId == userId))
            );

            if (result == null || result.Data == null || !result.Data.Any())
            {
                return PagedResultViewModel<EndpointDto>.Failure(
                    "No endpoint permissions assigned to this user.",
                    statusCode: 404
                );
            }

            return PagedResultViewModel<EndpointDto>.Success(
                data: result.Data,
                totalCount: result.TotalCount,
                pageNumber: result.PageNumber,
                pageSize: result.PageSize,
                message: "Endpoints that user has access:",
                statusCode: 200
            );
        }
    }
}
