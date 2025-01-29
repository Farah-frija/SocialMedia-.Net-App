﻿using Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public FileStorageService(IConfiguration configuration)
        {
            _basePath = configuration["FileStorage:BasePath"] ?? "wwwroot/uploads";

            // Ensure the base directory exists
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
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
                throw new InvalidOperationException("Failed to upload file", ex);
            }
        }

        public Task DeleteFileAsync(string filePath)
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
                throw new InvalidOperationException("Failed to delete file", ex);
            }
        }
    }

}
