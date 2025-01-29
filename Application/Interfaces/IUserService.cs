using Core.Application.DTOs;
using Core.Application.DTOs.DtoRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfileAsync(int userId);
        Task UpdatePartialProfileAsync(int userId, UpdateUserProfileDto updatedProfile);

    }
}
