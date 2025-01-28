using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.DTOs.DTOsRequests.Identity;
using Core.Domain.Entities;


namespace Core.Application.Mapper.UserMapper
{
    partial class UserProfile
    {
        public void RegisterRequest()
        {

            CreateMap<RegisterUserDTO, User>();
           
        }
    }
}