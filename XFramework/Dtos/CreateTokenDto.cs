namespace Dtos
{
    public class CreateTokenDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public List<string> Role { get; set; }
    }
}
