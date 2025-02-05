using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace Core.Domain.Entities
{
   
    public class User: IdentityUser
      {
    public string? Biography { get; set; }
    public DateOnly? Birthday { get; set; }
        public bool IsPrivateProfile { get; set; } = true; // Default to private
        public string? ProfilePictureUrl { get; set; }
        public virtual ICollection<Follow> Followings { get; set; }
     public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<ChatRoom> chatrooms { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public IEnumerable<Story> Stories { get; set; }
    }

}
