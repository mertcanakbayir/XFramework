
namespace XFramework.DAL.Entities
{
    public class UserRole : AuditEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }


    }
}
