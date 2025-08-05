using AutoMapper;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFM.DAL.Entities;

namespace XFM.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile() { 
        
            CreateMap<User, UserDto>();
            
        }
    }
}
