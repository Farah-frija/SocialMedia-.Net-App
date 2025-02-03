using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Chat
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using global::Core.Domain.Entities;

    namespace Core.Domain.ServiceInterfaces.Chats
    {
        public interface IChatRoomService
        {
          
            Task<IdentityResult> CreateChatRoomAsync(ChatRoom chatRoom, List<string> usersIds);
            Task<IdentityResult> AddUserToChatRoomAsync(string chatRoomId, string userId);
            Task<IdentityResult> RemoveUserFromChatRoomAsync(string chatRoomId, string userId, string requesterId);
            Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId);
        }
    }

}
