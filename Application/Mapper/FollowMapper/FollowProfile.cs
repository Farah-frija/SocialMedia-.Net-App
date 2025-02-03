using AutoMapper;
using Core.Application.DTOs.DTOsRequests.FollowRequests;
using Core.Application.DTOs.DTOsRequests.Identity;
using Core.Application.DTOs.DTOsResponses.FollowResponse;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper.FollowMapper
{
    public class FollowProfile:Profile
    {
       public  FollowProfile()
        {
             CreateMap<GetFollowDto, Follow>();
            CreateMap<User,listingFollowersDto >();
        }
    }
}
