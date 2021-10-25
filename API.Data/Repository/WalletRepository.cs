using API.Common;
using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using API.Data.Model;
using API.Data.Specification;
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

        public async Task<Wallet> GetAsync(WalletSpecification specification)
        {
            return await _applicationContext.Wallets
                .Include(c => c.Transactions)
                .Include(c => c.WalletEvents).Where(specification.Criteria).FirstOrDefaultAsync();
        }

        public async Task<Wallet> GetByIdAndUserIdAsync(string walletId, string userId)
        {
            var res = await _applicationContext.Wallets.Where(c => c.Id  == walletId.ToLower() && c.UserId.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
            return res;
        }

        public async Task<Wallet> GetByIdAndUserIdAsync(WalletSpecification specification)
        {
            var res = await _applicationContext.Wallets.Where(specification.Criteria).FirstOrDefaultAsync();
            return res;
        }

        public async Task<Wallet> GetByIdAsync(string id)
        {
            return await _applicationContext.Wallets.FindAsync(id);
        }

        public async Task<Wallet> GetByUserIdAsync(string userId)
        {
           return await _applicationContext.Wallets.Where(c => c.UserId.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
        }

        public void Update(Wallet model)
        {
            _applicationContext.Wallets.Update(model);
        }
    }
}
