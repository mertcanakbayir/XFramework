using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL;
using XFramework.DAL.Entities;

namespace XFramework.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
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

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _xfmContext.AddRangeAsync(entities);
            await _xfmContext.SaveChangesAsync();
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

        public async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _xfmContext.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
            if (entities == null)
            {
                throw new KeyNotFoundException("Kayıtlar Bulunamadı.");
            }
            foreach (var entity in entities)
            {
                entity.IsActive = false;
            }
            _xfmContext.UpdateRange(entities);
            await _xfmContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            bool includeInactive = false, bool asNoTracking = false,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null,
            int? pageNumber = null,
            int? pageSize = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool orderByDescending = false)
        {
            var query = _xfmContext.Set<TEntity>().AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(e => e.IsActive);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var totalCount = await query.CountAsync();
            if (orderBy != null)
            {
                query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }
            else if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.OrderBy(e => e.Id);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
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
                query = query.Where(e => e.IsActive);
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
            var existingEntity = await _xfmContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Güncellenecek nesne veritabanında bulunamadı.");

            if (entity.Revision != null)
            {
                var currentRevision = Convert.ToInt32(existingEntity.Revision);
                var incomingRevision = Convert.ToInt32(entity.Revision);


                if (currentRevision != incomingRevision)
                {
                    throw new InvalidOperationException("Kayıt başka kullanıcı tarafından güncellenmiş.");
                }

                entity.Revision = currentRevision += 1;
            }
            _xfmContext.Set<TEntity>().Attach(entity);
            _xfmContext.Entry(entity).State = EntityState.Modified;
            await _xfmContext.SaveChangesAsync();
        }
    }
}
