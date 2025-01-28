using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsResponses
{
    public class LoginUserDtoResponse
    {
        public IdentityResult Result { get; set; }
        public Object? token { get; set; }
       
    }
}
