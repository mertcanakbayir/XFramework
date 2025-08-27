using Microsoft.AspNetCore.Identity;

namespace XFramework.BLL.Utilities.Hashing
{
    public class HashingHelper : IHashingHelper
    {
        private readonly PasswordHasher<object> _passwordHasher = new();
        public string HashPassword(string plainedPassword)
        {
            return _passwordHasher.HashPassword(null, plainedPassword);
        }
        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
