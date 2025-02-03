using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Chats;
using Infrastructure.Context;
using Infrastructure.Security;

namespace Infrastructure.Repositories.Chat
{
    public class MessageRepository : ImessageRepo
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Message>> GetChatRoomMessagesAsync(string chatRoomId)
        {
            if (string.IsNullOrWhiteSpace(chatRoomId))
            {
                throw new ArgumentException("Chat room ID cannot be null or empty.", nameof(chatRoomId));
            }

            try
            {
                // Vérifie si la salle de discussion existe
                var chatRoomExists = await _context.ChatRooms.AnyAsync(c => c.Id == chatRoomId);
                if (!chatRoomExists)
                {
                    throw new KeyNotFoundException($"Chat room with ID {chatRoomId} not found.");
                }

                // Récupère les messages de la salle de discussion
                var messages = await _context.Messages
                    .Where(m => m.ChatRoomId == chatRoomId)
                    .Include(m => m.Sender) // Ensure Sender is loaded
    .OrderBy(m => m.CreatedAt)
                   
                    .ToListAsync();


                /*foreach (var message in messages)
                {
                    message.Content = EncryptionHelper.Decrypt(message.Content);
                }*/

                return messages;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving chat room messages.", ex);
            }
        }

        public async Task<IdentityResult> SendMessageAsync(Message message)
        {
            
            try
            {
                var chatRoomExists = await _context.ChatRooms.AnyAsync(c => c.Id == message.ChatRoomId);
                if (!chatRoomExists)
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Chat room with ID {message.ChatRoomId} not found." });
                }

                var senderExists = await _context.Users.AnyAsync(u => u.Id == message.SenderId);
                if (!senderExists)
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Sender with ID {message.SenderId} not found." });
                }
                //message.Content = EncryptionHelper.Encrypt(message.Content);

                message.Id = Guid.NewGuid().ToString("N");
                message.CreatedAt = DateTime.UtcNow;
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Failed to send message: {ex.InnerException}" });
            }
        }
    }
}

