using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DtoRequests
{
    public class UpdateStoryDto
    {
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
