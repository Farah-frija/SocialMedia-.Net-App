using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.Chats
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Core.Domain.Entities;

    using Core.Domain.RepositoryInterfaces.Chats;
    using Core.Application.Interfaces.Chat.Core.Domain.ServiceInterfaces.Chats;

    namespace Application.Services.Chats
    {
        public class ChatRoomService : IChatRoomService
        {
            private readonly IChatRoomRepository _chatRoomRepository;

            public ChatRoomService(IChatRoomRepository chatRoomRepository)
            {
                _chatRoomRepository = chatRoomRepository;
            }

            // Create a new chat room
            public async Task<IdentityResult> CreateChatRoomAsync(ChatRoom chatRoom, List<string> userIds)
            {

            return await _chatRoomRepository.CreateChatRoomAsync(chatRoom, userIds);
            }

            // Add a user to a chat room
            public async Task<IdentityResult> AddUserToChatRoomAsync(string chatRoomId, string userId)
            {
                if (string.IsNullOrEmpty(chatRoomId) || string.IsNullOrEmpty(userId))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Chat room ID or user ID cannot be null or empty." });
                }

                return await _chatRoomRepository.AddUserToChatRoomAsync(chatRoomId, userId);
            }

            // Remove a user from a chat room
            public async Task<IdentityResult> RemoveUserFromChatRoomAsync(string chatRoomId, string userId, string requesterId)
            {
                if (string.IsNullOrEmpty(chatRoomId) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(requesterId))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Chat room ID, user ID, or requester ID cannot be null or empty." });
                }

                return await _chatRoomRepository.RemoveUserFromChatRoomAsync(chatRoomId, userId, requesterId);
            }

            // Get all chat rooms for a user
            public async Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId)
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new List<ChatRoom>(); // Return an empty list if the user ID is invalid
                }

                return await _chatRoomRepository.GetUserChatRoomsAsync(userId);
            }
        }
    }

}
