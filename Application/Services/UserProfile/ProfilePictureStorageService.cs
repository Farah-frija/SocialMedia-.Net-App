using Core.Application.Interfaces.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Application.Services.UserProfile
{
    public class ProfilePictureStorageService : IProfilePictureStorageService
    {
        private readonly string _basePath;

        public ProfilePictureStorageService(IConfiguration configuration)
        {
            _basePath = configuration["FileStorage:ProfilePicturesPath"] ?? "wwwroot/profile_pictures";

            // Ensure the base directory exists
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> UploadProfilePictureAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            try
            {
                var sanitizedFileName = Path.GetFileName(file.FileName); // Prevent directory traversal attacks
                var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}";
                var fullPath = Path.Combine(_basePath, uniqueFileName);

                // Save the file
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return uniqueFileName; // Return relative file path
            }
            catch (Exception ex)
            {
                // Log the error (logging implementation depends on your setup)
                throw new InvalidOperationException("Failed to upload profile picture", ex);
            }
        }

        public Task DeleteProfilePictureAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("File path cannot be null or empty");
                }

                var fullPath = Path.Combine(_basePath, filePath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log the error
                throw new InvalidOperationException("Failed to delete profile picture", ex);
            }
        }
    }
}
