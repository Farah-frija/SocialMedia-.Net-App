using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Chat
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Core.Domain.Entities;
    using Core.Domain.RepositoryInterfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Domain.RepositoryInterfaces.Chats;
    using Infrastructure.Context;

    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new chat room
        public async Task<IdentityResult> CreateChatRoomAsync(ChatRoom chatRoom, List<string> memberIds)
        {
            if (chatRoom == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Chat room cannot be null." });
            }

            chatRoom.Id = Guid.NewGuid().ToString("N");

            // Fetch users based on IDs
            var users = await _context.Users.Where(u => memberIds.Contains(u.Id)).ToListAsync();

            if (users.Count != memberIds.Count)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Some users were not found." });
            }

            chatRoom.Mambers = users; // Assign the users to the chat room

            _context.ChatRooms.Add(chatRoom);

            try
            {
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Failed to create chat room: {ex.InnerException?.Message}" });
            }
        }


        // Add a user to a chat room
        public async Task<IdentityResult> AddUserToChatRoomAsync(string chatRoomId, string userId)
        {
            var chatRoom = await _context.ChatRooms.Include(c => c.Mambers)
                                                   .FirstOrDefaultAsync(c => c.Id == chatRoomId);
            if (chatRoom == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Chat room not found." });
            }

            if (!chatRoom.isGroup)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Cannot add users to a non-group chat room." });
            }
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            if (chatRoom.Mambers.Any(u => u.Id == userId))
            {
                return IdentityResult.Failed(new IdentityError { Description = "User is already in the chat room." });
            }

            chatRoom.Mambers.Add(user);


            try
            {
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to add user to chat room." });
            }
        }

        // Remove a user from a chat room
        public async Task<IdentityResult> RemoveUserFromChatRoomAsync(string chatRoomId, string userId, string requesterId)
        {
            var chatRoom = await _context.ChatRooms.Include(c => c.Mambers)
                                                   .FirstOrDefaultAsync(c => c.Id == chatRoomId);

            if (chatRoom == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Chat room not found." });
            }

            if (chatRoom.CreatedBy != requesterId)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Only the creator can remove users from the chat room." });
            }

            var userToRemove = chatRoom.Mambers.FirstOrDefault(u => u.Id == userId);
            if (userToRemove == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found in the chat room." });
            }

            chatRoom.Mambers.Remove(userToRemove);

            try
            {
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed(new IdentityError { Description = "Failed to remove user from chat room." });
            }
        }

        // Get all chat rooms for a user
        public async Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId)
        {
            return await _context.ChatRooms
                .Include(c => c.Mambers)
                .Where(c => c.Mambers.Any(u => u.Id == userId))
                .ToListAsync();
        }
    }

}
