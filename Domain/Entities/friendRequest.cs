using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
   
        public class FriendRequest
        {
            public string Id { get; private set; }
            public string SenderId { get; private set; }
            public string ReceiverId { get; private set; }
            public FriendRequestStatus Status { get; private set; }
            public DateTime CreatedAt { get; private set; }
            public DateTime? UpdatedAt { get; private set; }
          
        // Navigation properties
                          
             public User Sender { get; private set; }
             
             public User Receiver { get; private set; }
        }
}
