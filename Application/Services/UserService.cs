using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.Domain.RepositoryInterfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            return new UserProfileDto
            {
                UserName = user.UserName,
                Biography = user.Biography,
                ProfilePicture = user.ProfilePicture,
                Location = user.Location,
                Birthday = user.Birthday,
                Email = user.Email,
              
            };
        }
        public async Task UpdatePartialProfileAsync(int userId, UpdateUserProfileDto updatedProfile)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            // Update only provided properties
            if (!string.IsNullOrWhiteSpace(updatedProfile.Username))
                user.UserName = updatedProfile.Username;

            if (!string.IsNullOrWhiteSpace(updatedProfile.Bio))
                user.Biography = updatedProfile.Bio;

            if (!string.IsNullOrWhiteSpace(updatedProfile.ProfilePicture))
                user.ProfilePicture = updatedProfile.ProfilePicture;

            if (!string.IsNullOrWhiteSpace(updatedProfile.Location))
                user.Location = updatedProfile.Location;

            if (updatedProfile.DateOfBirth.HasValue)
                user.Birthday = updatedProfile.DateOfBirth.Value;

            if (!string.IsNullOrWhiteSpace(updatedProfile.Email))
                user.Email = updatedProfile.Email;

            

            await _userRepository.UpdateAsync(user);
        }


    }
}
