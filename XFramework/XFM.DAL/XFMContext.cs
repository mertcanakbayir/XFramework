using Microsoft.EntityFrameworkCore;
using XFM.DAL.Entities;

namespace XFM.DAL
{
    public class XFMContext:DbContext
    {

        public XFMContext(DbContextOptions<XFMContext> options)
       : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(XFMContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
