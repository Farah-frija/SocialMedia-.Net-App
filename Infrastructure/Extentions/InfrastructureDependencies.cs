using Core.Application.Interfaces.Follows;
using Core.Application.Interfaces.Identity;
using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces.Chats;
using Core.Domain.RepositoryInterfaces.Follows;
using Infrastructure.Context;

using Infrastructure.Identity.Services;

using Infrastructure.Repositories.Chat;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
    public static class InfrastructureDependencies
    {

        public static void AddInfrastructureIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders(); // Adds token providers for password recovery, etc.
            services.AddScoped<IAuthService, AuthService>();


            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "2018274258585644"; // Remplacez par l'App ID Facebook
                facebookOptions.AppSecret = "771fb742fbd1716449ef990fbdd75a20"; // Remplacez par l'App Secret Facebook
            });
           



        }
        public static void AddInfrastructureRepoInjections(this IServiceCollection services)
        {
            services.AddScoped<IFollowRepo, FollowRepo>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<ImessageRepo, MessageRepository>();


        }





        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              configuration.GetConnectionString("WebApplication1Context")
          )
      );
        }

    }
}
