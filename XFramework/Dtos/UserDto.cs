namespace Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; init; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int Revision { get; set; }
    }
}
