using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
   
        public class Follow
        {
            public string Id { get;  set; }
            public string SenderId { get;  set; }
            public string ReceiverId { get;  set; }
        public FollowRequestStatus Status { get;  set; } = FollowRequestStatus.Pending;
           public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            //public DateTime? UpdatedAt { get; private set; }
          
        // Navigation properties
                          
             public User Sender { get;  set; }
             
             public User Receiver { get;  set; }
        }
}
