using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using XFramework.DAL;
using XFramework.DAL.Entities;
using XFramework.Helper.Exceptions;
using XFramework.Helper.Models;
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
        public async Task<PagedResult<TDto>> GetAllAsync<TDto>(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool orderByDescending = false,
            int? pageNumber = null,
            int? pageSize = null,
            bool includeInactive = false,
            bool asNoTracking = true)
        {
            var query = _xfmContext.Set<TEntity>().AsQueryable();
            if (!includeInactive)
                query = query.Where(e => e.IsActive);

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            var totalCount = await query.CountAsync();

            if (orderBy != null)
                query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            else if (pageNumber.HasValue && pageSize.HasValue)
                query = query.OrderBy(e => e.Id);

            if (pageNumber.HasValue && pageSize.HasValue)
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);

            if (asNoTracking)
                query = query.AsNoTracking();

            var data = await query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();

            return new PagedResult<TDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? totalCount
            };
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            bool includeInactive = false,
            bool asNoTracking = true)
        {

            var query = _xfmContext.Set<TEntity>().AsQueryable();

            if (!includeInactive)
                query = query.Where(e => e.IsActive);

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(); ;
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
                    throw new RevisionConflictException(" [CONFLICT] Record updated by another user.");
                }

                entity.Revision = currentRevision += 1;
            }
            _xfmContext.Set<TEntity>().Attach(entity);
            _xfmContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
