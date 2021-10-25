using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using API.Data.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class TransactionRepo : Repository<Transaction>, ITransactionRepo
    {
        private readonly ApplicationContext _applicationContext;
        public TransactionRepo(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Transaction> GetTransactionAsync(TransactionSpecification specification)
        {
            return await _applicationContext.Transactions
          .Include(c => c.Wallet)
          .Where(specification.Criteria).FirstOrDefaultAsync();
        }

        public async Task<List<Transaction>> ListTransactionsAsync(TransactionSpecification specification)
        {
            return await _applicationContext.Transactions
          .Include(c => c.Wallet)
          .Where(specification.Criteria).ToListAsync();
        }
    }
}
