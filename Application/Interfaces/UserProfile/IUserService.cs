using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Interfaces.UserProfile
{
    public interface IUserService
    {
        Task<User?> GetUserProfileAsync(string userId);
        Task UpdateBiographyAsync(string userId, string biography);
        Task UpdateBirthdayAsync(string userId, DateOnly birthday);
        Task TogglePrivateProfileAsync(string userId);
        Task UpdateProfilePictureAsync(string userId, IFormFile file);

        // Add methods for updating username and password
        Task<bool> UpdateUsernameAsync(string userId, string newUsername);
        Task<bool> UpdatePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
