using Core.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class PrivacySettings
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }  // User who owns these settings
        public PrivacyLevel PostVisibility { get; set; } = PrivacyLevel.Public;  // Who can see the user's posts
        public PrivacyLevel CommentVisibility { get; set; } = PrivacyLevel.Public;  // Who can see the user's comments
        public PrivacyLevel FollowVisibility { get; set; } = PrivacyLevel.Public;  // Who can follow the user
        public PrivacyLevel FriendRequestVisibility { get; set; } = PrivacyLevel.Public;  // Who can send friend requests to the user

        public virtual User User { get; set; }
    }
}
