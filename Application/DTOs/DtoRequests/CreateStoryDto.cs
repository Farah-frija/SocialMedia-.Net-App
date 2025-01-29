using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DtoRequests
{
    public class CreateStoryDto
    {
        public string Content { get; set; }
        public IFormFile? Image { get; set; } // Image file upload

        public Guid UserId { get; set; }
    }
}
