using Core.Application.DTOs.DTOsRequests;
using Core.Application.DTOs.DTOsResponses;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentDto> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) throw new Exception("Comment not found");

            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                UserName = comment.User?.UserName,
                CreatedAt = comment.CreatedAt
            };
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPostAsync(Guid postId)
        {
            var comments = await _commentRepository.GetCommentsByPostAsync(postId);

            return comments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                UserId = comment.UserId,
                UserName = comment.User?.UserName,
                CreatedAt = comment.CreatedAt
            });
        }

        public async Task CreateAsync(CreateCommentDto createCommentDto)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Content = createCommentDto.Content,
                UserId = createCommentDto.UserId,
                PostId = createCommentDto.PostId
            };

            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateAsync(Guid id, string content)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) throw new Exception("Comment not found");

            comment.Content = content;
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}
