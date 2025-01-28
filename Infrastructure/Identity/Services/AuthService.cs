using AutoMapper;
using Core.Application.DTOs.DTOsResponses;
using Core.Application.Interfaces.Identity;
using Core.Domain.Entities;
using Infrastructure.Identity.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JWTBearerTokenSetting jwtBearerTokenSettings;

        public AuthService(UserManager<User> userManager, IMapper mapper, IOptions<JWTBearerTokenSetting> jwtTokenOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
        }

        
        public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        {
            // Check if the user already exists by email
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            }

            var existingUsername = await _userManager.FindByNameAsync(user.UserName);
            if (existingUsername != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username already in use." });
            }


            // Map the domain user to the ApplicationUser (Identity model)
            //var applicationUser = _mapper.Map<ApplicationUser>(user);
       


            // Attempt to create the user in the system
            return await _userManager.CreateAsync(user, password);
        }


        public async Task<LoginUserDtoResponse> LoginUserAsync(User user, string password)
        {
            var errors = new List<IdentityError>();

            // Check if the user exists by email
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);
            if (applicationUser == null)
            {
                errors.Add(new IdentityError { Code = "InvalidEmail", Description = "Email does not exist." });
                return new LoginUserDtoResponse
                {
                    Result = IdentityResult.Failed(errors.ToArray()),
            
                  
                };
            }

            // Check if the password is correct
            if (!await _userManager.CheckPasswordAsync(applicationUser, password))
            {
                errors.Add(new IdentityError { Code = "InvalidPassword", Description = "Incorrect password." });
                return new LoginUserDtoResponse
                {
                    Result = IdentityResult.Failed(errors.ToArray()),
                  
                 
                };
            }



            var token = GenerateToken(applicationUser);

            // If login is successful, return success, user ID, and user object
            return new LoginUserDtoResponse
            {
                Result = IdentityResult.Success,
                token = token,
               
            };
        }
        /*public async Task<LoginUserDtoResponse> HandleExternalLoginAsync(ClaimsPrincipal externalUser)
        {
            properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }*/

        private object GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpireTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (applicationUser == null)
            {
                throw new Exception("User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);

            return result.Succeeded;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());
            return applicationUser != null ? _mapper.Map<User>(applicationUser) : null;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var applicationUsers = _userManager.Users.ToList();
            return applicationUsers.Select(user => _mapper.Map<User>(user));
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId.ToString());
            if (applicationUser == null)
            {
                throw new Exception("User not found.");
            }

            var result = await _userManager.DeleteAsync(applicationUser);
            return result.Succeeded;
        }

        
    }

}
