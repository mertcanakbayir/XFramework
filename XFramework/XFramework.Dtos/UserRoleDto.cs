namespace XFramework.Dtos
{
    public class UserRoleDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = new();

        public List<string> Roles { get; set; } = new();
    }
}
