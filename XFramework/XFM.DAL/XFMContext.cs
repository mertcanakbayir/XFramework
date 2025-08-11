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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(XFMContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
