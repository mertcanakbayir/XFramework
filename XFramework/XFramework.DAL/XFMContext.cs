using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;
using XFramework.Helper.Enums;
using XFramework.Helper.Helpers;

namespace XFramework.DAL
{
    public class XFMContext : DbContext
    {
        private readonly CurrentUserProvider _currentUserProvider;
        public XFMContext(DbContextOptions<XFMContext> options, CurrentUserProvider currentUserProvider)
       : base(options)
        {
            _currentUserProvider = currentUserProvider;
        }

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


            modelBuilder.Entity<Endpoint>().HasData(
                new Endpoint { Id = 1, Controller = "User", Action = "GetAll", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 2, Controller = "Page", Action = "Get", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 3, Controller = "Page", Action = "Create", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 11, Controller = "Mail", Action = "SendMail", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 12, Controller = "Test", Action = "GetEnumTests", HttpMethod = "GET", IsActive = true },
                new Endpoint { Id = 13, Controller = "Mail", Action = "SendMailMQ", HttpMethod = "POST", IsActive = true },

                new Endpoint { Id = 4, Controller = "Role", Action = "AddUserRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 5, Controller = "Role", Action = "AddPageRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 6, Controller = "Role", Action = "AddEndpointRole", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 7, Controller = "Page", Action = "AddPage", HttpMethod = "POST", IsActive = true },
                new Endpoint { Id = 8, Controller = "Endpoint", Action = "AddEndpoint", HttpMethod = "POST", IsActive = true }

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
             new EndpointRole { EndpointId = 4, RoleId = 1 },
             new EndpointRole { EndpointId = 5, RoleId = 1 },
             new EndpointRole { EndpointId = 6, RoleId = 1 },

             new EndpointRole { EndpointId = 1, RoleId = 2 },
             new EndpointRole { EndpointId = 2, RoleId = 2 },

             new EndpointRole { EndpointId = 2, RoleId = 3 },

             new EndpointRole { EndpointId = 11, RoleId = 1 },
             new EndpointRole { EndpointId = 12, RoleId = 1 },
             new EndpointRole { EndpointId = 13, RoleId = 1 }

         );



            modelBuilder.Entity<SystemSetting>().HasData(
    new SystemSetting
    {
        Id = 1,
        Name = "Ayar 1",
        Description = "Ayar denemesi için açıklama 1",
        IsActive = true
    },
    new SystemSetting
    {
        Id = 2,
        Name = "Ayar 2",
        Description = "Ayar denemesi için açıklama 2",
        IsActive = true
    },
    new SystemSetting
    {
        Id = 3,
        Name = "Mail Ayarları",
        Description = "SMTP mail gönderim ayarları",
        IsActive = true
    },
    new SystemSetting
    {
        Id = 4,
        Name = "Log Ayarları",
        Description = "Sistem Log Ayarları"
    }
);



            modelBuilder.Entity<SystemSettingDetail>().HasData(
                new SystemSettingDetail
                {
                    Id = 1,
                    SystemSettingId = 1,
                    Key = "Ayar1Detail",
                    Value = "Merhaba",
                    Type = SystemSettingType.String,
                    IsActive = true
                },
                new SystemSettingDetail
                {
                    Id = 2,
                    SystemSettingId = 1,
                    Key = "Ayar2Detail",
                    Value = "2025-08-15 10:30:00",
                    Type = SystemSettingType.DateTime,
                    IsActive = true
                }, new SystemSettingDetail
                {
                    Id = 3,
                    SystemSettingId = 3,
                    Key = "SmtpHost",
                    Value = "smtp.freesmtpservers.com",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 4,
                    SystemSettingId = 3,
                    Key = "SmtpPort",
                    Value = "25",
                    Type = SystemSettingType.Int,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 5,
                    SystemSettingId = 3,
                    Key = "SmtpUser",
                    Value = "",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 6,
                    SystemSettingId = 3,
                    Key = "EncryptedPassword",
                    Value = "",
                    Type = SystemSettingType.String,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 7,
                    SystemSettingId = 3,
                    Key = "EnableSsl",
                    Value = "false",
                    Type = SystemSettingType.Bool,
                    IsActive = true
                },

                new SystemSettingDetail
                {
                    Id = 8,
                    SystemSettingId = 3,
                    Key = "SenderEmail",
                    Value = "deneme@mertcan.com",
                    Type = SystemSettingType.String,
                    IsActive = true
                },
                new SystemSettingDetail
                {
                    Id = 9,
                    SystemSettingId = 3,
                    Key = "IsQueue",
                    Value = "false",
                    IsActive = true
                },
                new SystemSettingDetail
                {
                    Id = 10,
                    SystemSettingId = 4,
                    Key = "IsEnabled",
                    Type = SystemSettingType.Bool,
                    Value = "true",
                    IsActive = true
                }
                            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    RoleId = 1,
                    UserId = 1,
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@test.com",
                    Username = "admin",
                    CreatedAt = new DateTime(1111 - 11 - 11),
                    UpdatedAt = new DateTime(1111 - 11 - 11),
                    CreatedBy = 1,
                    Password = "AQAAAAIAAYagAAAAEBV9CdhPcsGb7++CWXFC+hFKkqkxKR7LfvNFEgWzEBolNu1bW3WXvLd5FF/mCcDwAw=="
                }

            );


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
