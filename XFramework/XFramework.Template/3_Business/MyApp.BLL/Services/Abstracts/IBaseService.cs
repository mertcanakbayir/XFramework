using System.Linq.Expressions;
using MyApp.Helper.ViewModels;

namespace MyApp.BLL.Services.Abstracts
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
        Task<PagedResultViewModel<TDto>> GetAllAsync(Expression<Func<TDto, bool>>? filter = null, int? pageNumber = null,
            int? pageSize = null);
    }
}
