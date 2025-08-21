
namespace XFramework.DAL.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<EndpointRole> EndpointRoles { get; set; } = new List<EndpointRole>();

        public ICollection<PageRole> PageRoles { get; set; } = new List<PageRole>();
    }
}
