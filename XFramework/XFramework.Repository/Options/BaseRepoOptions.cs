using System.Linq.Expressions;

namespace XFramework.Repository.Options
{
    public class BaseRepoOptions<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>>? Filter { get; set; }
        public bool IncludeInactive { get; set; } = false;
        public bool AsNoTracking { get; set; } = false;
        public Func<IQueryable<TEntity>, IQueryable<TEntity>>? IncludeFunc { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public Expression<Func<TEntity, DateOnly>>? OrderBy { get; set; }
        public bool OrderByDescending { get; set; } = false;
        public int? TotalCount { get; internal set; }
    }
}
