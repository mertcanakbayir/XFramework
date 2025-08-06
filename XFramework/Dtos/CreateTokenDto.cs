namespace Dtos
{
    public class CreateTokenDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime Expiration { get; set; }
    }
}
