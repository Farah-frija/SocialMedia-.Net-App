using Core.Domain.Entities;
using System;


namespace Core.Domain.RepositoryInterfaces.Stories
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