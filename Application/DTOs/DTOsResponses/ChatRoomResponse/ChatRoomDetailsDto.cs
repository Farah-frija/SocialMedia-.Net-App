using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsResponses.ChatRoomResponse
{
    public class ChatRoomDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public int MembersCount { get; set; }
    }

}
