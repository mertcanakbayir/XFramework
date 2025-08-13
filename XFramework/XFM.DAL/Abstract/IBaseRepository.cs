using System.Linq.Expressions;

namespace XFM.DAL.Abstract
{
    public interface IBaseRepository<T> where T:class
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity); 

        Task DeleteAsync(int id);

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter=null!, bool includeInactive=false,bool asNoTracking=false, Func<IQueryable<T>,IQueryable<T>> includeFunc=null!);
        Task<T> GetAsync(Expression<Func<T, bool>> filter=null!, bool includeInactive=false,bool asNoTracking=false, Func<IQueryable<T>,IQueryable<T>> includeFunc=null!);
    }
}
