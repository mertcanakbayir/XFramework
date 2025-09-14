using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL;
using XFramework.DAL.Entities;
using XFramework.Repository.Options;
using XFramework.Repository.Repositories.Abstract;

namespace XFramework.Repository.Repositories.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly XFMContext _xfmContext;
        private readonly IMapper _mapper;
        public BaseRepository(XFMContext xfmContext, IMapper mapper)
        {
            _xfmContext = xfmContext;
            _mapper = mapper;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _xfmContext.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _xfmContext.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var ent = await _xfmContext.FindAsync<TEntity>(id);
            if (ent == null)
            {
                throw new KeyNotFoundException("Record not found.");
            }
            if (ent is BaseEntity baseEntity)
            {
                baseEntity.IsActive = false;
                _xfmContext.Update(ent);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _xfmContext.Set<TEntity>().Where(e => ids.Contains(e.Id)).ToListAsync();
            if (entities == null)
            {
                throw new KeyNotFoundException("Records not found.");
            }
            foreach (var entity in entities)
            {
                entity.IsActive = false;
            }
            _xfmContext.UpdateRange(entities);
        }
        public async Task<List<TDto>> GetAllAsync<TDto>(BaseRepoOptions<TEntity>? options = null)
        {
            var query = _xfmContext.Set<TEntity>().AsQueryable();
            if (options == null)
            {
                options = new BaseRepoOptions<TEntity>();
            }
            if (!options.IncludeInactive)
            {
                query = query.Where(e => e.IsActive);
            }

            if (options.IncludeFunc != null)
            {
                query = options.IncludeFunc(query);
            }
            if (options.Filter != null)
            {
                query = query.Where(options.Filter);
            }
            if (options.OrderBy != null)
            {
                query = options.OrderByDescending ? query.OrderByDescending(options.OrderBy) : query.OrderBy(options.OrderBy);
            }
            else if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                query = query.OrderBy(e => e.Id);
            }

            if (options.PageNumber.HasValue && options.PageSize.HasValue)
            {
                var totalCount = await query.CountAsync();
                options.TotalCount = totalCount;
                query = query.Skip((options.PageNumber.Value - 1) * options.PageSize.Value).Take(options.PageSize.Value);
            }
            if (options.AsNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TEntity> GetAsync(BaseRepoOptions<TEntity>? options)
        {

            var query = _xfmContext.Set<TEntity>().AsQueryable();
            if (options == null)
            {
                options = new BaseRepoOptions<TEntity>();
            }
            if (!options.IncludeInactive)
            {
                query = query.Where(e => e.IsActive);
            }
            if (options.IncludeFunc != null)
            {
                query = options.IncludeFunc(query);
            }
            if (options.Filter != null)
            {
                query = query.Where(options.Filter);
            }
            if (options.AsNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var existingEntity = await _xfmContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (existingEntity == null)
                throw new KeyNotFoundException("Entity not found in the database.");

            if (entity.Revision != null)
            {
                var currentRevision = Convert.ToInt32(existingEntity.Revision);
                var incomingRevision = Convert.ToInt32(entity.Revision);

                if (currentRevision != incomingRevision)
                {
                    throw new InvalidOperationException("Record updated by another user.");
                }

                entity.Revision = currentRevision += 1;
            }
            _xfmContext.Set<TEntity>().Attach(entity);
            _xfmContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
