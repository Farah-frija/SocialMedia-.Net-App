using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Posts
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(string basePath)
        {
            _basePath = basePath;
        }

        public async Task<string> SaveFileAsync(string folderPath, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            // Create directory if it doesn't exist
            string directory = Path.Combine(_basePath, folderPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Generate a unique file name
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(directory, fileName);

            // Save the file to the disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative file path for the database
            return Path.Combine(folderPath, fileName).Replace("\\", "/");
        }

        public Task DeleteFileAsync(string filePath)
        {
            string fullPath = Path.Combine(_basePath, filePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }

}
