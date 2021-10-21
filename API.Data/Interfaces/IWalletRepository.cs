using API.Common;
using API.Data.Entities;
using API.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        public Task Create(Wallet model);
        public void Update(Wallet model);
        public Task<Wallet> GetByIdAndUserIdAsync(string walletId, string userId);
    }
}
