using AutoMapper;
using Core.Application.DTOs.DTOsResponses.StoryResponse;
using Core.Application.Interfaces.Stories;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Stories;

namespace Core.Application.Services.Stories
{
    public class StoryCommentService : IStoryCommentService
    {
        private readonly IStoryCommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public StoryCommentService(IStoryCommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<StoryCommentDto> AddCommentAsync(Guid storyId, string userId, string content)
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
            var comments = await _commentRepository.GetCommentsByStoryIdAsync(storyId);
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