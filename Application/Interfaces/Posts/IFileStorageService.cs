using Microsoft.AspNetCore.Http;  // Add this line to resolve IFormFile
using System.Threading.Tasks;
namespace Core.Application.Interfaces.Posts
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(string folderPath, IFormFile file);
        Task DeleteFileAsync(string filePath);
    }
}
