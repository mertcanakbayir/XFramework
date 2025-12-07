using AutoMapper;
using FluentValidation;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Dtos.Page;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class PageService : BaseService<Page, PageDto, PageAddDto, PageUpdateDto>, IRegister
    {

        public PageService(IValidator<PageAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Page> baseRepository, IUnitOfWork unitOfWork, IValidator<PageUpdateDto> updateDtoValidator) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
        }

        public async Task<PagedResultViewModel<PageDto>> GetPagesByUser(int userId)
        {
            if (userId <= 0)
            {
                return PagedResultViewModel<PageDto>.Failure("Invalrid user ID");
            }

            var pages = await _baseRepository.GetAllAsync<PageDto>(filter: p => p.PageRoles.Any(pr => pr.Role.UserRoles.Any(ur => ur.UserId == userId)));

            if (pages == null || pages.Data == null || !pages.Data.Any())
            {
                return PagedResultViewModel<PageDto>.Failure(
                    "User has no page permissions",
                    statusCode: 404
                );
            }

            return PagedResultViewModel<PageDto>.Success(
                pages.Data,
                pages.TotalCount,
                pages.PageNumber,
                pages.PageSize,
                "User accessible pages",
                200
            );
        }

    }
}
