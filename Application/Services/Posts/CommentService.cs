using Core.Application.Interfaces.Posts;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Services.Posts
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null) throw new Exception("Comment not found");
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostAsync(Guid postId)
        {
            return await _commentRepository.GetCommentsByPostAsync(postId);
        }

        public async Task CreateAsync(Comment comment)
        {
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