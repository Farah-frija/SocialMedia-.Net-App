using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests.MessageRequests
{
    public class SendMessageDto
    {
        [Required]
        public string Content { get; set; } // Contenu du message
        [Required]
        public string SenderId { get; set; } // Identifiant de l'utilisateur qui envoie le message
        [Required]
        public string ChatRoomId { get; set; } // Identifiant de la chat room
     
    }
}
