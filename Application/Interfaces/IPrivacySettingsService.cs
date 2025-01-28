using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public  interface IPrivacySettingsService
    {
        PrivacySettings GetPrivacySettings(Guid userId);
        void UpdatePrivacySettings(Guid userId, PrivacySettings updatedSettings);

    }
}
