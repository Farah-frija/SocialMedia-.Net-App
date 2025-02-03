using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Interfaces.Chat;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Chats;
using Microsoft.AspNetCore.Identity;

namespace Core.Application.Services.Chats
{
    public class ChatRoomMessageService : IMessageSerice

    {
        private readonly ImessageRepo _messageRepo;

        public ChatRoomMessageService(ImessageRepo messageRepo)
        {
            _messageRepo = messageRepo;
        }

        // Get messages of a specific chat room
        public async Task<List<Message>> GetChatRoomMessagesAsync(string chatRoomId)
        {
            return await _messageRepo.GetChatRoomMessagesAsync(chatRoomId);
        }

        // Send a message in a chat room
        public async Task<IdentityResult> SendMessageAsync(Message message)
        {
            // Create a new message
         

            // Send the message using the repository
            return await _messageRepo.SendMessageAsync(message);
        }
    }
}
