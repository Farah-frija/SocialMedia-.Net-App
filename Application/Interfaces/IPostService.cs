using Core.Application.DTOs;
using Core.Application.DTOs.DTOsRequests;
using Core.Application.DTOs.DTOsResponses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IPostService
    {
        Task<PostDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PostDto>> GetAllAsync();
        Task<IEnumerable<PostDto>> GetPostsByUserAsync(Guid userId);
        Task CreateAsync(CreatePostDto createPostDto, List<IFormFile> photoFiles);
        Task DeleteAsync(Guid id);
    }
}
