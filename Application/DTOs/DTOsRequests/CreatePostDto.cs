using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests
{
    public class CreatePostDto
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
       // public List<string> PhotoUrls { get; set; } // New property for photo URLs

    }
}
