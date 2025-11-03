using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using FluentValidation;
using MyApp.BLL.Services.Abstracts;
using MyApp.DAL.Entities;
using MyApp.Helper.ViewModels;
using MyApp.Repository.Repositories.Abstract;

namespace MyApp.BLL.Services.Concretes
{
    public class BaseService<TEntity, TDto, TAddDto, TUpdateDto> : IBaseService<TDto, TAddDto, TUpdateDto>
        where TAddDto : class
        where TUpdateDto : class
        where TDto : class
        where TEntity : BaseEntity
    {
        protected readonly IValidator<TAddDto> _addDtoValidator;
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IValidator<TUpdateDto> _updateDtoValidator;
        public BaseService(IValidator<TAddDto> addDtoValidator, IMapper mapper, IBaseRepository<TEntity> baseRepository, IUnitOfWork unitOfWork,
            IValidator<TUpdateDto> updateDtoValidator)
        {
            _addDtoValidator = addDtoValidator;
            _mapper = mapper;
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
            _updateDtoValidator = updateDtoValidator;
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
            if (dtos == null || !dtos.Any())
            {
                return ResultViewModel<string>.Failure("No data provided");
            }
            foreach (var dto in dtos)
            {
                var validationResult = _addDtoValidator.Validate(dto);
                if (!validationResult.IsValid)
                {
                    return ResultViewModel<string>.Failure("Please check credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                }
            }
            var entites = _mapper.Map<List<TEntity>>(dtos);
            await _baseRepository.AddRangeAsync(entites);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Added succesfully");
        }

        public async Task<ResultViewModel<string>> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return ResultViewModel<string>.Failure("Invalid id provided", errors: new List<string> { "Id must be greater than 0" });
            }
            var entity = await _baseRepository.GetAsync(e => e.Id == id);
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
            if (ids == null || !ids.Any())
            {
                return ResultViewModel<string>.Failure("No ids provided");
            }
            var entities = await _baseRepository.GetAllAsync<TDto>(filter: e => ids.Contains(e.Id));
            if (entities.TotalCount == 0)
            {
                return ResultViewModel<string>.Failure("No records found");
            }
            await _baseRepository.DeleteRangeAsync(ids);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Records deleted succesfully");
        }

        public async Task<PagedResultViewModel<TDto>> GetAllAsync(Expression<Func<TDto, bool>>? filter = null, int? pageNumber = null,
            int? pageSize = null)
        {
            Expression<Func<TEntity, bool>>? entityFilter = null;
            if (filter != null)
            {
                entityFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
            }
            var result = await _baseRepository.GetAllAsync<TDto>(filter: entityFilter, asNoTracking: true, pageSize: pageSize, pageNumber: pageNumber);
            if (result.TotalCount == 0)
            {
                return PagedResultViewModel<TDto>.Failure("No records found");
            }
            return PagedResultViewModel<TDto>.Success(
                data: result.Data,
                totalCount: result.TotalCount,
                pageNumber: result.PageNumber,
                pageSize: result.PageSize,
                message: "Records:",
                statusCode: 200);
        }

        public async Task<ResultViewModel<TDto>> GetAsync(Expression<Func<TDto, bool>>? filter = null)
        {
            Expression<Func<TEntity, bool>>? entityFilter = null;
            if (filter != null)
            {
                entityFilter = _mapper.MapExpression<Expression<Func<TEntity, bool>>>(filter);
            }
            var entity = await _baseRepository.GetAsync(filter: entityFilter, asNoTracking: true);
            if (entity == null)
            {
                return ResultViewModel<TDto>.Failure("No records found");
            }
            var dto = _mapper.Map<TDto>(entity);
            return ResultViewModel<TDto>.Success(dto, "Record:");
        }
        public async Task<ResultViewModel<string>> UpdateAsync(int id, TUpdateDto dto)
        {
            if (id <= 0)
            {
                return ResultViewModel<string>.Failure("Invalid id provided",
                    errors: new List<string> { "Id must be greater than 0" });
            }
            var validationResult = _updateDtoValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return ResultViewModel<string>.Failure("Check Credentials", errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var existing = await _baseRepository.GetAsync(filter: e => e.Id == id);
            if (existing == null)
            {
                return ResultViewModel<string>.Failure("Not Found", errors: new List<string> { $"Record with {id} does not exist." });
            }
            _mapper.Map(dto, existing);
            await _baseRepository.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return ResultViewModel<string>.Success(message: "Updated Succesfully");
        }
    }
}
