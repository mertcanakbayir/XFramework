using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;
using XFramework.Dtos.Page;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class PageService : BaseService<Page, PageDto, PageAddDto, PageUpdateDto>
    {
        private readonly IBaseRepository<User> _userRepository;

        public PageService(IValidator<PageAddDto> addDtoValidator, IMapper mapper, IBaseRepository<Page> baseRepository, IUnitOfWork unitOfWork, IValidator<PageUpdateDto> updateDtoValidator, IBaseRepository<User> userRepository) : base(addDtoValidator, mapper, baseRepository, unitOfWork, updateDtoValidator)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel<List<PageDto>>> GetPagesByUser(int userId)
        {
            var user = await _userRepository.GetAsync(new BaseRepoOptions<User>
            {
                Filter = q => q.Id == userId,
                AsNoTracking = true,
                IncludeFunc = q => q.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(pr => pr.PageRoles).ThenInclude(p => p.Page)
            });
            if (user == null)
            {
                return ResultViewModel<List<PageDto>>.Failure("User not found");
            }
            var pages = user.UserRoles.SelectMany(ur => ur.Role.PageRoles)
                .Select(pr => pr.Page)
                .Distinct()
                .ToList();
            var pagesDto = _mapper.Map<List<PageDto>>(pages);
            return ResultViewModel<List<PageDto>>.Success(pagesDto, "Pages the user have permission:");
        }
    }
}
