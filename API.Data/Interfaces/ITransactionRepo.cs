using API.Data.Entities;
using API.Data.Specification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface ITransactionRepo : IRepository<Transaction>
    {
        Task Create(Transaction model);
        void Update(Transaction model);
        Task<List<Transaction>> GetByUserId(string userId);
        Task<List<Transaction>> GetTransactions(TransactionSpecification specification);
    }
}
