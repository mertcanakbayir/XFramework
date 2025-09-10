using XFramework.DAL.Entities;
using XFramework.Repository.Options;

namespace XFramework.Repository.Repositories.Abstract
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<List<T>> GetAllAsync(BaseRepoOptions<T>? options = null);
        Task<T> GetAsync(BaseRepoOptions<T>? options);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task DeleteRangeAsync(IEnumerable<int> ids);
    }
}
