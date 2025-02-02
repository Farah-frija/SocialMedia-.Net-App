using System;
using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // User who created the post
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostPhoto> Photos { get; set; } = new List<PostPhoto>(); // New property
    }
}


