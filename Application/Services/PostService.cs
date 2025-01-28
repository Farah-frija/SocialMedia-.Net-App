using Core.Application.DTOs;
using Core.Application.DTOs.DTOsRequests;
using Core.Application.DTOs.DTOsResponses;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;  // Add this line to resolve IFormFile


namespace Core.Application.Services
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

        public async Task<PostDto> GetByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) throw new Exception("Post not found");

            return new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                CreatedAt = post.CreatedAt,
                PhotoUrls = post.Photos?.Select(p => p.Url).ToList() // Map Photos to PhotoUrls
            };
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                CreatedAt = post.CreatedAt,
                PhotoUrls = post.Photos?.Select(p => p.Url).ToList() // Map Photos to PhotoUrls
            });
        }

        public async Task<IEnumerable<PostDto>> GetPostsByUserAsync(Guid userId)
        {
            var posts = await _postRepository.GetPostsByUserAsync(userId);

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                Content = post.Content,
                UserId = post.UserId,
                UserName = post.User?.UserName,
                CreatedAt = post.CreatedAt,
                PhotoUrls = post.Photos?.Select(p => p.Url).ToList() // Map Photos to PhotoUrls

            });
        }


        public async Task CreateAsync(CreatePostDto createPostDto, List<IFormFile> photoFiles)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Content = createPostDto.Content,
                UserId = createPostDto.UserId
            };

            if (photoFiles != null && photoFiles.Count > 0)
            {
                foreach (var photoFile in photoFiles)
                {
                    // Save the photo and get the path
                    string photoPath = await _fileStorageService.SaveFileAsync("uploads/posts", photoFile);

                    // Create PostPhoto instance and add it to the Photos collection
                    post.Photos.Add(new PostPhoto { Id = Guid.NewGuid(), Url = photoPath });
                }
            }

            // Save the post in the repository
            await _postRepository.AddAsync(post);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }
}
