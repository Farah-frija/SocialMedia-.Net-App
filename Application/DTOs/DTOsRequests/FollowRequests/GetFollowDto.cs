using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests.FollowRequests
{
    public class GetFollowDto
    {
        //public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
