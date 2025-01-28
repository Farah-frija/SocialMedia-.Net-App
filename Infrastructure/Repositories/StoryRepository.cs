using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StoryRepository:IStoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddStory(Story story)
        {
            _dbContext.Stories.Add(story);
            _dbContext.SaveChanges();
        }
        public void DeleteStory(Story story)
        {
            
            if (story != null)
            {
                _dbContext.Stories.Remove(story);
                _dbContext.SaveChanges();
            }
        }
        public Story GetStory(Guid storyId)
        {
            return _dbContext.Stories
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == storyId);
        }
        public IEnumerable<Story> GetActiveStories(Guid userId)
        {
            return _dbContext.Stories
                .Where(s => s.UserId == userId && s.ExpiryTime > DateTime.UtcNow)
                .ToList();
        }
    }
}
