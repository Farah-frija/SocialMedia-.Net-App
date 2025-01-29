using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IStoryCommentRepository
    {
        Task AddAsync(StoryComment comment);
        Task<IEnumerable<StoryComment>> GetCommentsByStoryIdAsync(Guid storyId);
        Task<StoryComment> GetCommentByIdAsync(Guid commentId);
        Task UpdateCommentAsync(StoryComment comment);
        Task DeleteCommentAsync(Guid commentId);


    }
}
