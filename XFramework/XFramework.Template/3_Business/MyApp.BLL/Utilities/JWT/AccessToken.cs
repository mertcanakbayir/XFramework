using System.Security.Claims;

namespace MyApp.BLL.Utilities.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
