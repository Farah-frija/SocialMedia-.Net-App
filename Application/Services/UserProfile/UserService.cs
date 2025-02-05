using Microsoft.AspNetCore.Identity;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using System.Threading.Tasks;
using Core.Application.Interfaces.UserProfile;
using Core.Domain.RepositoryInterfaces.UserProfile;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services.UserProfile
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IProfilePictureStorageService _profilePictureStorageService;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, IProfilePictureStorageService profilePictureStorageService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _profilePictureStorageService = profilePictureStorageService;

        }

        public async Task<User?> GetUserProfileAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task UpdateBiographyAsync(string userId, string biography)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return;

            user.Biography = biography;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task UpdateBirthdayAsync(string userId, DateOnly birthday)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return;

            user.Birthday = birthday;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task TogglePrivateProfileAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return;

            user.IsPrivateProfile = !user.IsPrivateProfile;
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task UpdateProfilePictureAsync(string userId, IFormFile file)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return;

            // Upload new profile picture
            var profilePictureUrl = await _profilePictureStorageService.UploadProfilePictureAsync(file);

            // Update user profile picture URL
            user.ProfilePictureUrl = profilePictureUrl;
            await _userRepository.UpdateUserAsync(user);
        }

        // Update Username
        public async Task<bool> UpdateUsernameAsync(string userId, string newUsername)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Check if the username is available (you might want to validate this first)
            var usernameExist = await _userManager.FindByNameAsync(newUsername);
            if (usernameExist != null) return false;

            user.UserName = newUsername;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // Update Password
        public async Task<bool> UpdatePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            // Check the current password
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!checkPasswordResult) return false;

            // Update the password
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }
    }
}
