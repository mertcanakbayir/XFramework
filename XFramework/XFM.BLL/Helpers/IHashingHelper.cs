using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFM.BLL.HashService
{
    public interface IHashingHelper
    {
        string HashPassword(string plainedPassword);
        bool VerifyPassword(string hashedPassword, string plainedPassword);

    }
}
