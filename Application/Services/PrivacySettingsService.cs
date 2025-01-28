using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.RepositoryInterfaces;
using Core.Application.Interfaces;

namespace Core.Application.Services
{
    public class PrivacySettingsService:IPrivacySettingsService
    {
        private readonly IPrivacySettingsRepository _privacySettingsRepository;

        public PrivacySettingsService(IPrivacySettingsRepository privacySettingsRepository)
        {
            _privacySettingsRepository = privacySettingsRepository;
        }

        public PrivacySettings GetPrivacySettings(Guid userId)
        {
            return _privacySettingsRepository.GetPrivacySettingsByUserId(userId)
                   ?? throw new Exception("Privacy settings not found for the user.");
        }

        public void UpdatePrivacySettings(Guid userId, PrivacySettings updatedSettings)
        {
            var existingSettings = _privacySettingsRepository.GetPrivacySettingsByUserId(userId);

            if (existingSettings == null)
                throw new Exception("Privacy settings not found for the user.");

            // Update the settings
            existingSettings.PostVisibility = updatedSettings.PostVisibility;
            existingSettings.CommentVisibility = updatedSettings.CommentVisibility;
            existingSettings.FollowVisibility = updatedSettings.FollowVisibility;
            existingSettings.FriendRequestVisibility = updatedSettings.FriendRequestVisibility;

            _privacySettingsRepository.UpdatePrivacySettings(existingSettings);

        }
    }
}
