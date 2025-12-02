
namespace MyApp.Dtos.User
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Revision { get; set; }
        public int RoleId { get; set; }
    }
}