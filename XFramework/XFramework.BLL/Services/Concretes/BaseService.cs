using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using XFramework.BLL.Services.Abstracts;
using XFramework.DAL.Entities;
using XFramework.Helper.ViewModels;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.BLL.Services.Concretes
{
    public class BaseService<TEntity, TDto, TAddDto, TUpdateDto> : IBaseService<TDto, TAddDto, TUpdateDto, TEntity>
        where TAddDto : class
        where TUpdateDto : class
        where TDto : class
        where TEntity : BaseEntity
    {
        private readonly IValidator<TAddDto> _addDtoValidator;
        private readonly IValidator<List<TAddDto>> _rangeAddDtoValidator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<TUpdateDto> _updateDtoValidator;
        public BaseService(IValidator<TAddDto> addDtoValidator, IMapper mapper, IBaseRepository<TEntity> baseRepository, IValidator<List<TAddDto>> rangeAddDtoValidator, IUnitOfWork unitOfWork)
        {
            _addDtoValidator = addDtoValidator;
            _mapper = mapper;
            _baseRepository = baseRepository;
            _rangeAddDtoValidator = rangeAddDtoValidator;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultViewModel<string>> AddAsync(TAddDto dto)
        {
            var validationResult = _addDtoValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Please check credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var entity = _mapper.Map<TEntity>(dto);
            await _baseRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success("Added succesfully.", 200);
        }

        public async Task<ResultViewModel<string>> AddRangeAsync(List<TAddDto> dtos)
        {
            var validationResult = _rangeAddDtoValidator.Validate(dtos);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Please check credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var entites = _mapper.Map<List<TEntity>>(dtos);
            await _baseRepository.AddRangeAsync(entites);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Added succesfully");
        }

        public async Task<ResultViewModel<string>> DeleteAsync(int id)
        {
            var entity = await _baseRepository.GetAsync(new BaseRepoOptions<TEntity>
            {
                Filter = e => e.Id == id
            });
            if (entity == null)
            {
                return ResultViewModel<string>.Failure("Not Found");
            }
            await _baseRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Deleted succesfully");
        }

        public async Task<ResultViewModel<string>> DeleteRangeAsync(List<int> ids)
        {
            var entities = await _baseRepository.GetAllAsync(new BaseRepoOptions<TEntity>
            {
                Filter = e => ids.Contains(e.Id)
            });
            if (!entities.Any())
            {
                return ResultViewModel<string>.Failure("No records found");
            }
            await _baseRepository.DeleteRangeAsync(ids);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Records deleted succesfully");
        }

        public async Task<ResultViewModel<List<TDto>>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var entities = await _baseRepository.GetAllAsync(
                new BaseRepoOptions<TEntity>
                {
                    Filter = filter,
                    AsNoTracking = true
                });
            if (entities == null)
            {
                return ResultViewModel<List<TDto>>.Failure("No records found");
            }

            var dtos = _mapper.Map<List<TDto>>(entities);
            return ResultViewModel<List<TDto>>.Success(dtos, "Records:");
        }

        public async Task<ResultViewModel<TDto>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var options = new BaseRepoOptions<TEntity>
            {
                Filter = filter,
                AsNoTracking = true
            };
            var entity = await _baseRepository.GetAsync(options);
            if (entity == null)
            {
                return ResultViewModel<TDto>.Failure("No records found");
            }
            var dto = _mapper.Map<TDto>(entity);
            return ResultViewModel<TDto>.Success("Records:");
        }

        public async Task<ResultViewModel<TDto>> GetByIdAsync(int id)
        {
            var entity = await _baseRepository.GetAsync(new BaseRepoOptions<TEntity>
            {
                Filter = e => id == e.Id,
                AsNoTracking = true
            });
            if (entity == null)
                return ResultViewModel<TDto>.Failure("Not Found");

            var dto = _mapper.Map<TDto>(entity);
            return ResultViewModel<TDto>.Success(dto, "Success");
        }

        public async Task<PagedResultViewModel<TDto>> GetPagedAsync(Expression<Func<TEntity, bool>>? filter = null, int? pageNumber = 1, int? pageSize = 10)
        {
            var options = new BaseRepoOptions<TEntity>
            {
                Filter = filter,
                PageNumber = pageNumber,
                PageSize = pageSize,
                AsNoTracking = true
            };

            var entities = await _baseRepository.GetAllAsync(options);
            int totalCount = options.TotalCount ?? 0;

            var dtos = _mapper.Map<List<TDto>>(entities);
            return new PagedResultViewModel<TDto>
            {
                Data = dtos,
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 10,
                TotalCount = totalCount
            };
        }

        public async Task<ResultViewModel<string>> UpdateAsync(TUpdateDto dto)
        {
            var validationResult = _updateDtoValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Check Credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var entity = _mapper.Map<TEntity>(dto);
            await _baseRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Updated Succesfully");
        }
    }
}
