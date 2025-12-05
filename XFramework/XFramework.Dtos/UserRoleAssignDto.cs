namespace XFramework.Dtos
{
    public class UserRoleAssignDto
    {
        public int UserId { get; set; }

        public List<int> RoleIds { get; set; } = new();
    }
}
