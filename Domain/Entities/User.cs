using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
   
    public class User: IdentityUser
      {
    public string? Biography { get; set; }
    public DateOnly? Birthday { get; set; }
    public virtual List<Follow> Followings { get; set; }
        public virtual List<Follow> Followers { get; set; }
    }

}
