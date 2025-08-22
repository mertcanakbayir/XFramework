using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Abstract;
using XFramework.DAL.Entities;

namespace XFramework.DAL.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly XFMContext _xfmContext;
        public BaseRepository(XFMContext xfmContext)
        {
            _xfmContext = xfmContext;
        }
        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _xfmContext.AddAsync(entity);
                await _xfmContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("AddAsync sırasında hata oluştu.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _xfmContext.FindAsync<TEntity>(id);
            if (ent == null)
            {
                throw new KeyNotFoundException("Kayıt Bulunamadı.");
            }
            if (ent is BaseEntity baseEntity)
            {
                baseEntity.IsActive = false;
                _xfmContext.Update(ent);
                await _xfmContext.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            bool includeInactive = false, bool asNoTracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            var query = _xfmContext.Set<TEntity>().AsQueryable();

            if (!includeInactive && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                //    ((BaseEntity)(object)e).IsActive;
                //query = query.OfType<BaseEntity>()
                //.Where(e => e.IsActive)
                //.Cast<TEntity>();
                query = query.Where(e => ((BaseEntity)(object)e).IsActive);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, bool includeInactive = false, bool asNoTracking = false, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            var query = _xfmContext.Set<TEntity>().AsQueryable();

            if (!includeInactive && typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ////query = query.Where(e => e is BaseEntity baseEntity && baseEntity.IsActive);
                //query = query.OfType<BaseEntity>()
                //.Where(e => e.IsActive)
                //.Cast<TEntity>();
                query = query.Where(e => ((BaseEntity)(object)e).IsActive);
            }
            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public void GetCurrentUser(int userId)
        {
            _xfmContext.UserId = userId;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var entry = _xfmContext.Entry(entity);
            var id = entry.Property("Id").CurrentValue;
            var existingEntity = await _xfmContext.Set<TEntity>().FindAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek nesne veritabanında bulunamadı.");

            _xfmContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _xfmContext.SaveChangesAsync();
        }
    }
}
