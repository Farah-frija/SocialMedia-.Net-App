using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PrivacySettingsRepository:IPrivacySettingsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PrivacySettingsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PrivacySettings GetPrivacySettingsByUserId(Guid userId)
        {
            return _dbContext.PrivacySettings.FirstOrDefault(p => p.UserId == userId);
        }

        public void UpdatePrivacySettings(PrivacySettings privacySettings)
        {
            _dbContext.PrivacySettings.Update(privacySettings);
            _dbContext.SaveChanges();
        }

        public void CreatePrivacySettings(PrivacySettings privacySettings)
        {
            _dbContext.PrivacySettings.Add(privacySettings);
            _dbContext.SaveChanges();
        }

    }
}
