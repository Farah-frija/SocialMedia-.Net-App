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
        Task<Story> GetByIdAsync(Guid id);
        Task<IEnumerable<Story>> GetActiveStoriesByUserIdAsync(Guid userId);
        Task AddAsync(Story story);
        Task UpdateAsync(Story story);
        Task DeleteAsync(Guid id);
    }
}
