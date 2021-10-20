using API.Services.Services;
using API.Services.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    public static class ResolveServicesDependency
    {
        public static void RegisterAllServices(this IServiceCollection service)
        {
            service.AddScoped<IRegisterService, RegisterService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<IClaimService, ClaimService>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}

