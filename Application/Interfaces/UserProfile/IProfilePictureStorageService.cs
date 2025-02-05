using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.UserProfile
{
    public interface IProfilePictureStorageService
    {
        Task<string> UploadProfilePictureAsync(IFormFile file);
        Task DeleteProfilePictureAsync(string filePath);
    }
}
