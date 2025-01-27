using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs
{
    public class UpdateUserProfileDto
    {
        public string? Username { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Location { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Email { get; set; }
    }
}
