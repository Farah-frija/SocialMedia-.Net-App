using Core.Application.Interfaces.Posts;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Posts;


namespace Core.Application.Services.Posts
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task ToggleLikeAsync(Guid postId, string userId)
        {
            var existingLike = await _likeRepository.GetLikeAsync(postId, userId);

            if (existingLike != null)
            {
                await _likeRepository.RemoveLikeAsync(existingLike);
            }
            else
            {
                var newLike = new Like
                {
                    PostId = postId,
                    UserId = userId
                };
                await _likeRepository.AddLikeAsync(newLike);
            }
        }

        public async Task<int> GetLikeCountAsync(Guid postId)
        {
            return await _likeRepository.CountLikesAsync(postId);
        }
    }


}
