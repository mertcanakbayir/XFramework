using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.BLL.Result;

namespace XFM.BLL.Services.UserService
{
    public interface IHashService
    {
        string HashPassword(string plainedPassword);
        string VerifyPassword(string hashedPassword, string plainedPassword);

    }
}
