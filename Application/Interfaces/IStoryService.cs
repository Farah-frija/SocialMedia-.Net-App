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
        void AddStory( Guid userId,string content, int hoursToExpire);
        void DeleteStory( Guid storyId);
        IEnumerable<Story> GetActiveStories(Guid userId);
    }
}
