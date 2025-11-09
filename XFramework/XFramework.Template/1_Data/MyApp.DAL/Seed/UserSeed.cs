using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.DAL.Entities;

namespace MyApp.DAL.Seed
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
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
    }
}
