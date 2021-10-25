using API.Data;
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
        public static void ResolveRegisterDependencies(this IServiceCollection services)
        {
            services.ResolveRepositoryDependencies();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IClaimService, ClaimService>();
        }

        public static void ResolveLoginDependencies(this IServiceCollection services)
        {
            services.ResolveRepositoryDependencies();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IClaimService, ClaimService>();
        }

        public static void ResolveAdminDependencies(this IServiceCollection services)
        {
            services.ResolveRepositoryDependencies();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>();
        }

        public static void ResolveStockTraderDependencies(this IServiceCollection services)
        {
            services.ResolveRepositoryDependencies();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>();
        }
    }
}

