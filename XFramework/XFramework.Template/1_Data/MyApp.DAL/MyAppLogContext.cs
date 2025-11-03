using Microsoft.EntityFrameworkCore;
using MyApp.DAL.Entities;

namespace MyApp.DAL
{
    public class MyAppLogContext : DbContext
    {
        public MyAppLogContext(DbContextOptions<MyAppLogContext> options)
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
