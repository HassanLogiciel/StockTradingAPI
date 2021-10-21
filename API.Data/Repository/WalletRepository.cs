﻿using API.Common;
using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationContext _applicationContext;

        public WalletRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(Wallet model)
        {
            await _applicationContext.Wallets.AddAsync(model);
        }
        public async Task<List<Wallet>> GetAllAsync()
        {
            return await _applicationContext.Wallets.ToListAsync();
        }

        public async Task<Wallet> GetByIdAndUserIdAsync(string walletId, string userId)
        {
            var res = await _applicationContext.Wallets.Where(c => c.Id  == walletId.ToLower() && c.UserId.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
            return res;
        }

        public async Task<Wallet> GetByIdAsync(string id)
        {
            return await _applicationContext.Wallets.FindAsync(id);
        }

        public void Update(Wallet model)
        {
            _applicationContext.Wallets.Update(model);
        }
    }
}
