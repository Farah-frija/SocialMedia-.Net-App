using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IPrivacySettingsRepository
    {
        PrivacySettings GetPrivacySettingsByUserId(Guid userId);
        void UpdatePrivacySettings(PrivacySettings privacySettings);
        void CreatePrivacySettings(PrivacySettings privacySettings);
    }
}
