namespace XFramework.DAL.Entities
{
    public class EndpointRole : BaseEntity
    {
        public int EndpointId { get; set; }
        public Endpoint Endpoint { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
