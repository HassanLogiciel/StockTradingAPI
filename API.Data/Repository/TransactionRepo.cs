using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly ApplicationContext _applicationContext;
        public TransactionRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Create(Transaction model)
        {
            await _applicationContext.Transactions.AddAsync(model);
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            return await _applicationContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(string Id)
        {
            return await _applicationContext.FindAsync<Transaction>(Id);
        }

        public async Task<List<Transaction>> GetByUserId(string userId)
        {
            return await _applicationContext.Transactions
                .Include(c=>c.Wallet)
                .Where(c=>c.UserId == userId).ToListAsync();
        }

        public void Update(Transaction model)
        {
            _applicationContext.Transactions.Update(model);
        }
    }
}
