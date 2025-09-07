using System.Linq.Expressions;
using XFramework.DAL.Entities;

namespace XFramework.Repository.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>
            filter = null!, bool includeInactive = false, bool asNoTracking = false,
            Func<IQueryable<T>, IQueryable<T>> includeFunc = null!,
            int? pageNumber = null,
            int? pageSize = null,
             Expression<Func<T, object>> orderBy = null,
            bool orderByDescending = false
            );
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null!,
            bool includeInactive = false, bool asNoTracking = false,
            Func<IQueryable<T>, IQueryable<T>> includeFunc = null!);

        void GetCurrentUser(int userId);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task DeleteRangeAsync(IEnumerable<int> ids);
    }
}
