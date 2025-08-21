
namespace XFramework.DAL.Entities
{
    public class Endpoint : BaseEntity
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string HttpMethod { get; set; }

        public ICollection<EndpointRole> EndpointRoles { get; set; } = new List<EndpointRole>();
    }
}
