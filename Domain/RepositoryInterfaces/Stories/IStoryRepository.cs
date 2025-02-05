using Core.Domain.Entities;
using System;
using System.Collections.Generic;


namespace Core.Domain.RepositoryInterfaces.Stories
{
    public interface IStoryRepository
    {
        Task<Story> GetByIdAsync(Guid id);
        Task<IEnumerable<Story>> GetActiveStoriesByUserIdAsync(string userId);
        Task AddAsync(Story story);
        Task UpdateAsync(Story story);
        Task DeleteAsync(Guid id);
    }
}