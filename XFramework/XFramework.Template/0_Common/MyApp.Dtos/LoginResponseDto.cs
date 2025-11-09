namespace MyApp.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public List<string> UserPages { get; set; }
    }
}
