using Core.Application.DTOs.DtoRequests.StoryRequests;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Stories
{
    public interface IStoryService
    {
        Task<Story> AddStoryAsync(CreateStoryDto storyDto);
        Task<Story> UpdateStoryAsync(Guid id, UpdateStoryDto storyDto);
        Task<IEnumerable<Story>> GetActiveStoriesAsync(string userId);
        Task DeleteStoryAsync(Guid id);
    }
}