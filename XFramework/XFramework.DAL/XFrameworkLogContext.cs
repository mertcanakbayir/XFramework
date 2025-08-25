using Microsoft.EntityFrameworkCore;
using XFramework.DAL.Entities;

namespace XFramework.DAL
{
    public class XFrameworkLogContext : DbContext
    {
        public XFrameworkLogContext(DbContextOptions<XFrameworkLogContext> options)
         : base(options)
        {
        }
        public DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
