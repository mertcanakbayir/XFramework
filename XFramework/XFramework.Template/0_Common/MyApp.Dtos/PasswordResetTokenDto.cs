namespace MyApp.Dtos
{
    public class PasswordResetTokenDto
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
