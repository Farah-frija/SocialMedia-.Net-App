using AutoMapper;
using Core.Domain.Entities;
using Core.Application.DTOs;
using Core.Application.DTOs.DTOsRequests.MessageRequests;
using Core.Application.DTOs.DTOsResponses.MessageResponse;
using Core.Application.DTOs.DTOsResponses.ProfileResponse.UserAsIconDto;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<Message, ListOfMessagseDto>()
            .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => new UserAsIconInTheMessage
            {
                Id = src.Sender.Id,
                UserName = src.Sender.UserName
            }));

        CreateMap<SendMessageDto, Message>();

        CreateMap<Message, SendMessageDto>();
        //CreateMap<List<Message>, List<ListOfMessagseDto>>();
    }
}
