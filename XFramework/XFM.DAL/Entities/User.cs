using XFramework.DAL.Entities;

namespace XFM.DAL.Entities
{
    public class User:BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
