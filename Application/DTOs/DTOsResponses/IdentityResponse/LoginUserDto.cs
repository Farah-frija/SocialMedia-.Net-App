using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsResponses.IdentityResponse
{
    public class LoginUserDtoResponse
    {
        public IdentityResult Result { get; set; }
        public object? token { get; set; }

    }
}
