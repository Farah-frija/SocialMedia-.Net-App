using AutoMapper;
using Core.Application.DTOs.DTOsRequests.ChatRoomRequests;
using Core.Application.DTOs.DTOsRequests.FollowRequests;
using Core.Application.DTOs.DTOsResponses.ChatRoomResponse;
using Core.Application.DTOs.DTOsResponses.FollowResponse;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper.ChatRoomMapper
{
    public class ChatRoomProfile : Profile
    {
      
            public ChatRoomProfile()
            {
                CreateMap<CreateChatRoomDto, ChatRoom>();
            CreateMap <RemoveUserFromChatRoomDto, ChatRoom > ();
            CreateMap <AddUserToChatRoomDto, ChatRoom > ();
            CreateMap <ChatRoom, ChatRoomDto> ();
          
            }
       
    }
}
