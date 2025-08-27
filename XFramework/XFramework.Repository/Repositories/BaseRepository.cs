using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL;
using XFramework.DAL.Entities;

namespace XFramework.Repository.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class
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

            if (!includeInactive)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsActive"));
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

            if (!includeInactive)
            {
                query = query.Where(e => EF.Property<bool>(e, "IsActive"));
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
            var existingEntity = await _xfmContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == (int)_xfmContext.Entry(entity).Property("Id").CurrentValue);


            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek nesne veritabanında bulunamadı.");

            var revisionProperty = _xfmContext.Entry(existingEntity).Property("Revision");
            if (revisionProperty != null)
            {
                var currentRevision = Convert.ToInt32(revisionProperty.CurrentValue);
                var incomingRevision = Convert.ToInt32(_xfmContext.Entry(entity).Property("Revision").CurrentValue);


                if (currentRevision != incomingRevision)
                {
                    throw new InvalidOperationException("Kayıt başka kullanıcı tarafından güncellenmiş.");
                }

                _xfmContext.Entry(entity).Property("Revision").CurrentValue = currentRevision += 1;
            }
            _xfmContext.Set<TEntity>().Attach(entity);
            _xfmContext.Entry(entity).State = EntityState.Modified;
            await _xfmContext.SaveChangesAsync();
        }
    }
}
