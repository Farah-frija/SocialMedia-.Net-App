using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Identity
{
    public interface IAuthService
    {
       
            Task RegisterUserAsync(User user);
            Task<User> LoginUserAsync(User user);  // Accepting the User entity directly
          
        

    }
}
