using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests.ChatRoomRequests
{
    public class CreateChatRoomDto
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public bool IsGroup { get; set; }
        public List<string> MembersId { get; set; } // Membres de la chat room
    }
}
