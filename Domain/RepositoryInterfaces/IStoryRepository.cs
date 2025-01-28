using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IStoryRepository
    {
        void AddStory(Story story);
        void DeleteStory(Story story);
        Story GetStory(Guid storyId);
        IEnumerable<Story> GetActiveStories(Guid userId);
    }
}
