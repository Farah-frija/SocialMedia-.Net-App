using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsResponses.ProfileResponse
{
    public class ProfileDTO
    {
        public string? Username { get; set; }
        public string? Biography { get; set; }
        public DateOnly? Birthday { get; set; }
        public bool IsPrivateProfile { get; set; } = true;
        public string? ProfilePictureUrl { get; set; }
    }

}
