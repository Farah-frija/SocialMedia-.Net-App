using Core.Application.DTOs.DTOsRequests;
using Core.Application.DTOs.DTOsResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetByIdAsync(Guid id);
        Task<IEnumerable<CommentDto>> GetCommentsByPostAsync(Guid postId);
        Task CreateAsync(CreateCommentDto createCommentDto);
        Task UpdateAsync(Guid id, string content);
        Task DeleteAsync(Guid id);
    }
}
