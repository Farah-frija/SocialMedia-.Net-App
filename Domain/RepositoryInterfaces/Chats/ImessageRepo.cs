using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces.Chats
{
   public interface ImessageRepo
 
        {
           
            Task<List<Message>> GetChatRoomMessagesAsync(string chatRoomId);
            Task<IdentityResult> SendMessageAsync(Message message);
        }
    


}
