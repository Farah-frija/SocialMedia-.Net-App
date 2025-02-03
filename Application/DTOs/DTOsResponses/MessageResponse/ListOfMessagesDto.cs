using Core.Application.DTOs.DTOsResponses.ProfileResponse.UserAsIconDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsResponses.MessageResponse
{
    public class ListOfMessagseDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string ChatRoomId { get; set; }
        public UserAsIconInTheMessage Sender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
