using Core.Application.DTOs;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IStoryService
    {
        Task<Story> CreateStoryAsync(CreateStoryDto dto, Guid userId);
        Task<Story> UpdateStoryAsync(UpdateStoryDto dto, Guid userId);
        void DeleteStory( Guid storyId);
        IEnumerable<Story> GetActiveStories(Guid userId);
    }
}
