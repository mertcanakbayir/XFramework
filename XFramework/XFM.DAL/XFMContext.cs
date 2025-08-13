using Microsoft.EntityFrameworkCore;
using XFM.DAL.Entities;
using XFramework.DAL.Entities;

namespace XFM.DAL
{
    public class XFMContext:DbContext
    {

        public XFMContext(DbContextOptions<XFMContext> options)
       : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Endpoint> Endpoints { get; set; }

        public DbSet<EndpointRole> EndpointRoles { get; set; }

        public DbSet<PageRole> PageRoles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Page> Pages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(XFMContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", IsActive = true },
                new Role { Id = 2, Name = "Moderator", IsActive = true },
                new Role { Id = 3, Name = "User", IsActive = true }
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 }, // admin -> Admin
                new UserRole { UserId = 2, RoleId = 2 }, // moderator -> Moderator
                new UserRole { UserId = 3, RoleId = 3 }  // user -> User
            );

            modelBuilder.Entity<Endpoint>().HasData(
                new Endpoint { Id = 1, Controller = "User", Action = "GetAll", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 2, Controller = "Page", Action = "Get", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 3, Controller = "Page", Action = "Create", HttpMethod = "POST", IsActive = true }
            );
            modelBuilder.Entity<Page>().HasData(
                new Page { Id = 1, PageUrl = "/dashboard", IsActive = true },
                new Page { Id = 2, PageUrl = "/users", IsActive = true }
            );

            modelBuilder.Entity<PageRole>().HasData(
                new PageRole { PageId = 1, RoleId = 1 },
                new PageRole { PageId = 2, RoleId = 1 },

                new PageRole { PageId = 1, RoleId = 2 },

                new PageRole { PageId = 1, RoleId = 3 }
            );

            modelBuilder.Entity<EndpointRole>().HasData(
             new EndpointRole { EndpointId = 1, RoleId = 1 },
             new EndpointRole { EndpointId = 2, RoleId = 1 },
             new EndpointRole { EndpointId = 3, RoleId = 1 },

             new EndpointRole { EndpointId = 1, RoleId = 2 },
             new EndpointRole { EndpointId = 2, RoleId = 2 },

             new EndpointRole { EndpointId = 2, RoleId = 3 }
         );

        }

    }
}
