

namespace Core.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; } // Post this comment belongs to
        public string UserId { get; set; } // User who made the comment
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
