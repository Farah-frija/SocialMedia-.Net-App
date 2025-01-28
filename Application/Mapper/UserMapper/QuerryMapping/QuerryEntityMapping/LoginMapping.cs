using AutoMapper;
using Core.Application.DTOs.DTOsRequests.Identity;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper.UserMapper
{
   partial class UserProfile : Profile
    {  
        
        public void loginRequestMapping() { CreateMap<LoginUserDto, User>(); }
       
    }
}
