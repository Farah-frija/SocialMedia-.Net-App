using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
