using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Extentions
{

  
   
        public static class ApplicationDependencies
        {
            public static void AddApplicationServices(this IServiceCollection services)
            {
                //services.AddScoped<IAuthService, AuthService>();
                //services.AddScoped<IUserService, UserService>();
            }


            public static void AddAutoMapper(this IServiceCollection services)
            {

           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        }
    }

