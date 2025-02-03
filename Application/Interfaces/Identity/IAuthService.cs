using Core.Application.DTOs.DTOsResponses.IdentityResponse;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(User user, string password);
        Task<LoginUserDtoResponse> LoginUserAsync(User user,string password );  // Accepting the User entity directly
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(Guid userId);
        //Task<LoginUserDtoResponse> HandleExternalLoginAsync(ClaimsPrincipal externalUser);



    }
}
