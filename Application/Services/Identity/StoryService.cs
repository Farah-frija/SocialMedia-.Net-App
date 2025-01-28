using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.Identity
{
    public class StoryService:IStoryService
    {
        private readonly IStoryRepository _storyRepository;

        public StoryService(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }
        public void AddStory(Guid userId, string content, int hoursToExpire)
        {
            var story = new Story
            {
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddHours(hoursToExpire)
            };

            _storyRepository.AddStory(story);
        }
        public void DeleteStory( Guid storyId)
        {
            var story = _storyRepository.GetStory(storyId);

            if (story == null )
            {
                throw new InvalidOperationException("Story not found .");
            }

            _storyRepository.DeleteStory(story);
        }

        public IEnumerable<Story> GetActiveStories(Guid userId)
        {
            return _storyRepository.GetActiveStories(userId);
        }
    }
}
