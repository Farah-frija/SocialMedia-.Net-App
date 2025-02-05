using Core.Domain.Entities;


namespace Core.Domain.RepositoryInterfaces.Posts
{
    public interface ILikeRepository
    {
        Task<Like> GetLikeAsync(Guid postId, string userId);
        Task AddLikeAsync(Like like);
        Task RemoveLikeAsync(Like like);
        Task<int> CountLikesAsync(Guid postId);
    }


}
