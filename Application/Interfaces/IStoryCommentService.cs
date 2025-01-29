using Core.Application.DTOs.DTOsResponses;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Application.Interfaces
{
    public interface IStoryCommentService
    {
        Task<StoryCommentDto> AddCommentAsync(Guid storyId, Guid userId, string content);
        Task<IEnumerable<StoryCommentDto>> GetCommentsByStoryAsync(Guid storyId);
        Task<StoryCommentDto> UpdateStoryCommentAsync(Guid commentId, string newContent);
        Task DeleteStoryCommentAsync(Guid commentId);
    }
}
