using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Chats;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Chat
{
    public  interface IMessageSerice
    {
        public  Task<List<Message>> GetChatRoomMessagesAsync(string chatRoomId);
        public Task<IdentityResult> SendMessageAsync(Message message);

    }
}
