using AutoMapper;
using Core.Application.DTOs.DTOsResponses;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Application.Services
{
    public class StoryCommentService:IStoryCommentService
    {
        private readonly IStoryCommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public StoryCommentService(IStoryCommentRepository commentRepository,IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<StoryCommentDto> AddCommentAsync(Guid storyId, Guid userId, string content)
        {
            var comment = new StoryComment
            {
                StoryId = storyId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            return _mapper.Map<StoryCommentDto>(comment);
        }

        public async Task<IEnumerable<StoryCommentDto>> GetCommentsByStoryAsync(Guid storyId)
        {
            var comments=await _commentRepository.GetCommentsByStoryIdAsync(storyId);
            return comments.Select(comment => _mapper.Map<StoryCommentDto>(comment));
        }
        public async Task<StoryCommentDto> UpdateStoryCommentAsync(Guid commentId, string newContent)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
                throw new ArgumentException("Comment not found");

            // Update the comment content
            comment.Content = newContent ?? comment.Content; // If newContent is null, retain the old content
            comment.UpdatedAt = DateTime.UtcNow;

            // Save changes to the repository
            await _commentRepository.UpdateCommentAsync(comment);
            return _mapper.Map<StoryCommentDto>(comment);
        }
        public async Task DeleteStoryCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);
            if (comment == null)
                throw new ArgumentException("Comment not found");

            // Delete the comment
            await _commentRepository.DeleteCommentAsync(commentId);
        }
    }
}
