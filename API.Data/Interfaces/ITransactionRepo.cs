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
        Task<List<Transaction>> ListTransactionsAsync(TransactionSpecification specification);
        Task<Transaction> GetTransactionAsync(TransactionSpecification specification);
    }
}
