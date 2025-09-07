using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;
using XFramework.Dtos;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Repositories;

namespace XFramework.BLL.Services.Concretes
{
    public class PageService
    {
        private readonly IBaseRepository<Page> _pageRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IValidator<PageAddDto> _pageAddDtoValidator;
        private readonly CurrentUserService _currentUserService;
        //private readonly IValidator<ForgotPasswordDto> _forgotPasswordDtoValidator;

        public PageService(IBaseRepository<Page> pageRepository, IMapper mapper, IBaseRepository<User> userRepository, IValidator<PageAddDto> pageAddDtoValidator, CurrentUserService currentUserService)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _pageAddDtoValidator = pageAddDtoValidator;
            _currentUserService = currentUserService;
        }

        public async Task<ResultViewModel<string>> AddPage(PageAddDto pageAddDto)
        {
            var validationResult = _pageAddDtoValidator.Validate(pageAddDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ResultViewModel<string>.Failure("Lütfen Girdiğiniz bilgileri kontrol edin.", errors, 400);
            }
            var pageEntity = _mapper.Map<Page>(pageAddDto);
            _pageRepository.GetCurrentUser(_currentUserService.GetUserId());
            _pageRepository.AddAsync(pageEntity);
            return ResultViewModel<string>.Success("Sayfa Eklendi", 200);
        }

        public async Task<ResultViewModel<List<PageDto>>> GetPagesByUser(int userId)
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId, includeFunc: i => i.Include(e => e.UserRoles).ThenInclude(u => u.Role).ThenInclude(p => p.PageRoles).ThenInclude(pr => pr.Page));
            if (user == null)
            {
                return ResultViewModel<List<PageDto>>.Failure("Kullanıcıya henüz sayfa yetkisi atanmamış", statusCode: 400);
            }
            var pages = user.UserRoles.Select(ur => ur.Role).SelectMany(pr => pr.PageRoles).Select(p => p.Page).ToList();
            var pagesDto = _mapper.Map<List<PageDto>>(pages);
            return ResultViewModel<List<PageDto>>.Success(pagesDto, "Kullanıcı Sayfaları:", 200);
        }

        public async Task<ResultViewModel<string>> UpdatePage(PageAddDto pageAddDto)
        {
            var page = await _pageRepository.GetAsync(e => e.Id == pageAddDto.Id);
            if (page == null)
            {
                return ResultViewModel<string>.Failure("Güncellenecek sayfa bulunamadı", statusCode: 400);
            }
            var pageEntity = _mapper.Map<Page>(pageAddDto);
            _pageRepository.GetCurrentUser(_currentUserService.GetUserId());
            await _pageRepository.UpdateAsync(pageEntity);
            return ResultViewModel<string>.Success("Sayfa güncellendi", statusCode: 200);
        }

        public async Task<ResultViewModel<List<PageDto>>> GetAllPages()
        {
            var pages = await _pageRepository.GetAllAsync();
            if (pages == null)
            {
                return ResultViewModel<List<PageDto>>.Failure("Sayfa bulunamadı.", null, 400);
            }

            var pageDto = _mapper.Map<List<PageDto>>(pages);
            return ResultViewModel<List<PageDto>>.Success(pageDto, "Başarılı", 200);
        }
    }

}

