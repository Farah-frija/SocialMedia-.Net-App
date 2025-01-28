
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs
{
    public class CreateStoryDto
    {
        public string Content { get; set; }
        public IFormFile? Image { get; set; } // Image file upload
        public TimeSpan? ExpiryDuration { get; set; } // Optional expiry duration
    }
}
