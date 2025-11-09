using System.Security.Claims;

namespace XFramework.BLL.Utilities.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
