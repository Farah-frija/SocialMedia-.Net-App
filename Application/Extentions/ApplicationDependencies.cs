using Core.Application.Interfaces.Chat;
using Core.Application.Interfaces.Chat.Core.Domain.ServiceInterfaces.Chats;
using Core.Application.Interfaces.Follows;
using Core.Application.Mapper.UserMapper;
using Core.Application.Services.Chats;
using Core.Application.Services.Chats.Application.Services.Chats;
using Core.Domain.RepositoryInterfaces.Chats;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Extentions
{

  
   
        public static class ApplicationDependencies
        {
            public static void AddApplicationServices(this IServiceCollection services)
            {
               services.AddScoped<IFollowService, FollowService>();
               services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IMessageSerice, ChatRoomMessageService>();
        }
            

        
        }
    }

