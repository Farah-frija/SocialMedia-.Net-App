using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Guid id);
    }
}
