using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests.ChatRoomRequests
{
    public class RemoveUserFromChatRoomDto
    {
        public string ChatRoomId { get; set; }
        public string UserId { get; set; }
        public string RequesterId { get; set; }
    }

}
