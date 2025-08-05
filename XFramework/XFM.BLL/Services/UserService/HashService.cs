using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFM.BLL.Services.UserService
{
    public class HashService : IHashService
    {
        private readonly PasswordHasher<object> _passwordHasher = new();

        public string HashPassword(string plainedPassword)
        {
             return _passwordHasher.HashPassword(null, plainedPassword);
        }

        public string VerifyPassword(string plainPassword, string hashedPassword)
        {
            var result =  _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword);

            return result.ToString();
        }
    }
}
