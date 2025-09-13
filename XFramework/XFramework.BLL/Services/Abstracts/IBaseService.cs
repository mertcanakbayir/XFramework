using System.Linq.Expressions;
using XFramework.Helper.ViewModels;

namespace XFramework.BLL.Services.Abstracts
{
    public interface IBaseService<TDto, TAddDto, TUpdateDto> where TUpdateDto : class
        where TAddDto : class
        where TDto : class
    {
        Task<ResultViewModel<string>> AddAsync(TAddDto dto);
        Task<ResultViewModel<string>> AddRangeAsync(List<TAddDto> dtos);
        Task<ResultViewModel<string>> UpdateAsync(int id, TUpdateDto dto);
        Task<ResultViewModel<string>> DeleteAsync(int id);
        Task<ResultViewModel<string>> DeleteRangeAsync(List<int> ids);
        Task<ResultViewModel<TDto>> GetAsync(Expression<Func<TDto, bool>>? filter = null);
        Task<ResultViewModel<List<TDto>>> GetAllAsync(Expression<Func<TDto, bool>>? filter = null);
        Task<PagedResultViewModel<TDto>> GetPagedAsync(Expression<Func<TDto, bool>>? filter = null, int? pageNumber = 1, int? pageSize = 10);
    }
}
