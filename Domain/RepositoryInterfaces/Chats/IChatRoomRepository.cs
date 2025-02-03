using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces.Chats
{
    public interface IChatRoomRepository
    {
        Task<IdentityResult> CreateChatRoomAsync(ChatRoom chatRoom, List<string> userIds);
        Task<IdentityResult> AddUserToChatRoomAsync(string chatRoomId, string userId);
        Task<IdentityResult> RemoveUserFromChatRoomAsync(string chatRoomId, string userId, string requesterId);
        Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId);
    }
}
