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
    public class WalletRepository : Repository<Wallet> , IWalletRepository
    {
        private readonly ApplicationContext _applicationContext;

        public WalletRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<Wallet> GetWalletAsync(WalletSpecification specification)
        {
            return await _applicationContext.Wallets
                .Include(c => c.Transactions)
                .Include(c => c.WalletEvents).Where(specification.Criteria).FirstOrDefaultAsync();
        }

        public async Task<List<Wallet>> ListWalletAsync(WalletSpecification specification)
        {
            return await _applicationContext.Wallets
               .Include(c => c.Transactions)
               .Include(c => c.WalletEvents).Where(specification.Criteria).ToListAsync();
        }
    }
}
