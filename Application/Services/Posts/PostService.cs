using Core.Application.Interfaces.Posts;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Posts;
using Microsoft.AspNetCore.Http;


namespace Core.Application.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileStorageService _fileStorageService;

        public PostService(IPostRepository postRepository, IFileStorageService fileStorageService)
        {
            _postRepository = postRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) throw new Exception("Post not found");
            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserAsync(string userId)
        {
            return await _postRepository.GetPostsByUserAsync(userId);
        }

        public async Task CreateAsync(Post post, List<IFormFile> photoFiles)
        {
            if (photoFiles != null && photoFiles.Count > 0)
            {
                foreach (var photoFile in photoFiles)
                {
                    string photoPath = await _fileStorageService.SaveFileAsync("uploads/posts", photoFile);
                    post.Photos.Add(new PostPhoto { Id = Guid.NewGuid(), Url = photoPath });
                }
            }

            await _postRepository.AddAsync(post);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }
}