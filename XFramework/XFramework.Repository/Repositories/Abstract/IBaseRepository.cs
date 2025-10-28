using System.Linq.Expressions;
using XFramework.DAL.Entities;
using XFramework.Helper.Models;
using XFramework.Helper.ViewModels;

namespace XFramework.Repository.Repositories.Abstract
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<PagedResult<TDto>> GetAllAsync<TDto>(
             Expression<Func<T, bool>>? filter = null,
             Func<IQueryable<T>, IQueryable<T>>? include = null,
             Expression<Func<T, object>>? orderBy = null,
             bool orderByDescending = false,
             int? pageNumber = null,
             int? pageSize = null,
             bool includeInactive = false,
             bool asNoTracking = true);

        Task<T?> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            bool includeInactive = false,
            bool asNoTracking = true);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task DeleteRangeAsync(IEnumerable<int> ids);
    }
}
