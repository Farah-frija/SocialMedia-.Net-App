using Core.Application.DTOs.DtoRequests;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IFileStorageService _fileStorageService;

        public StoryService(IStoryRepository storyRepository, IFileStorageService fileStorageService)
        {
            _storyRepository = storyRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Story> AddStoryAsync(CreateStoryDto storyDto)
        {
            // Save image if present
            string imageUrl = null;
            if (storyDto.Image != null)
            {
                imageUrl = await _fileStorageService.UploadFileAsync(storyDto.Image);
            }
            // Calculate the expiry time (now + duration)
            var expiryTime = DateTime.UtcNow.AddHours(24);
            // Create the story
            var story = new Story
            {
                Id = Guid.NewGuid(),
                UserId = storyDto.UserId,
                Content = storyDto.Content,
                ImageUrl = imageUrl,
                ExpiryTime = expiryTime,
                CreatedAt = DateTime.UtcNow
            };

            await _storyRepository.AddAsync(story);
            return story;
        }

        public async Task<Story> UpdateStoryAsync(Guid id, UpdateStoryDto storyDto)
        {
            var story = await _storyRepository.GetByIdAsync(id);
            if (story == null)
                throw new ArgumentException("Story not found");

            // Update content and image if provided
            story.Content = storyDto.Content ?? story.Content;
            if (storyDto.Image != null)
            {
                story.ImageUrl = await _fileStorageService.UploadFileAsync(storyDto.Image);
            }

            story.UpdatedAt = DateTime.UtcNow;
            await _storyRepository.UpdateAsync(story);
            return story;
        }

        public async Task<IEnumerable<Story>> GetActiveStoriesAsync(Guid userId)
        {
            return await _storyRepository.GetActiveStoriesByUserIdAsync(userId);
        }

        public async Task DeleteStoryAsync(Guid id)
        {
            var story = await _storyRepository.GetByIdAsync(id);
            if (story != null && !string.IsNullOrEmpty(story.ImageUrl))
            {
                await _fileStorageService.DeleteFileAsync(story.ImageUrl);
            }

            await _storyRepository.DeleteAsync(id);
        }
    }



}


