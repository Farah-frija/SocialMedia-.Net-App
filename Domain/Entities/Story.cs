using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Story
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // Foreign key to the user who created the story
        public string Content { get; set; } // URL or base64 string for images/videos
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryTime { get; set; } // When the story expires
        public bool IsActive => DateTime.UtcNow < ExpiryTime; // Helper to check if the story is active

        // Navigation property
        public User User { get; set; }

    }
}
