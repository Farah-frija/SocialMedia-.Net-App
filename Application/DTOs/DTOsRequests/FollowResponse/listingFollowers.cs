using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.DTOsRequests.FollowResponse
{
    public class listingFollowersDto
    {
        public string UserName { get; set; }
        public string id { get; set; } // You may want to include the user ID for further use or navigation
    } 
}
