using AutoMapper;
using Core.Application.DTOs.DTOsResponses;
using Core.Application.DTOs.DTOsResponses.StoryResponse;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapper.StoryMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StoryComment, StoryCommentDto>();
        }
    }
}