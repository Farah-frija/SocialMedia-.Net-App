using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;
   
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string ProfilePicture { get; set; }
        public string Location { get; set; }

        public string? Biography { get; set; }
        public DateOnly? Birthday { get; set; }

        // Optionally, add other domain-specific properties such as role, avatar, etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Record when the user is created
    }

}
