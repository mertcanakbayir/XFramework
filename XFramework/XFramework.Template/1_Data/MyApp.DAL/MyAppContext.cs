using Microsoft.EntityFrameworkCore;
using MyApp.DAL.Entities;
using MyApp.Helper.Helpers;

namespace MyApp.DAL
{
    public class MyAppContext : DbContext
    {
        private readonly CurrentUserProvider _currentUserProvider;
        public MyAppContext(DbContextOptions<MyAppContext> options, CurrentUserProvider currentUserProvider)
        : base(options)
        {
            _currentUserProvider = currentUserProvider;
        }

        // Built-in framework entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<EndpointRole> EndpointRoles { get; set; }
        public DbSet<PageRole> PageRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Page> Pages { get; set; }

        //Generated entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int userId = _currentUserProvider.GetUserId();
            this.ChangeTracker.DetectChanges();
            var added = this.ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();
            foreach (var entity in added)
            {
                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedAt = DateTime.Now;
                    baseEntity.CreatedBy = userId;

                    baseEntity.UpdatedAt = DateTime.Now;
                    baseEntity.UpdatedBy = userId;
                }

            }

            var modified = this.ChangeTracker.Entries()
                           .Where(t => t.State == EntityState.Modified)
                           .ToArray();
            foreach (var entry in modified)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    var originalIsActive = entry.OriginalValues.GetValue<bool>(nameof(BaseEntity.IsActive));
                    var currentIsActive = baseEntity.IsActive;

                    if (originalIsActive != currentIsActive)
                    {
                        baseEntity.DeletedAt = DateTime.Now;
                        baseEntity.DeletedBy = userId;
                    }
                    baseEntity.UpdatedAt = DateTime.Now;
                    baseEntity.UpdatedBy = userId;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
