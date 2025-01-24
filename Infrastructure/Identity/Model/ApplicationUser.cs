﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Model
{
   
        public class ApplicationUser : IdentityUser<Guid>
        {
        public string? Biography { get; set; }
        public DateOnly? Birthday { get; set; }
    }

    
}
