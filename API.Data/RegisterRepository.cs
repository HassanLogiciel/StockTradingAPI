using API.Data.Interfaces;
using API.Data.Model;
using API.Data.Repository;
using API.Services.Services.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data
{
    public static class RegisterRepository
    {
        public static void ResolveRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEnityBase, EntityBase>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepo, TransactionRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppSettingsRepo, AppSettingsRepo>();
        }
    }
}
