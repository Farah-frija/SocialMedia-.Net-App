using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class PostPhoto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; } // Foreign key to Post
        public string Url { get; set; } // URL of the photo
        public Post Post { get; set; } // Navigation property
    }
}
