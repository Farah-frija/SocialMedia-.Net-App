using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string ProfilePicture { get; set; }
        public string Location { get; set; }

        public string? Biography { get; set; }
        public DateOnly? Birthday { get; set; }
    }
}
