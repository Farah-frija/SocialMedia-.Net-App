

namespace Core.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public string UserId { get; set; }

        public Post Post { get; set; } // Navigation Property
        public User User { get; set; } // Navigation Property
    }

}
