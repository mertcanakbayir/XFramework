namespace XFramework.DAL.Entities
{
    public class PageRole : BaseEntity
    {
        public int PageId { get; set; }

        public Page Page { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
