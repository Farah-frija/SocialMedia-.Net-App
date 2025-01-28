using Infrastructure.Data;
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
            //services.AddScoped<IAuthService, AuthService>();
            //services.AddScoped<IUserService, UserService>();
        }


        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {

  
            services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("WebApplication1Context"
)));
        }
    }
}
