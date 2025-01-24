using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper.UserMapper
{
    public  partial class UserProfile : Profile
    {

        public UserProfile()
        {
          GetUsersListMapping();
        }

    }
}
