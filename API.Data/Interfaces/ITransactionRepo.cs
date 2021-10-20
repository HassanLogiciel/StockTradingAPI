using API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Interfaces
{
    public interface ITransactionRepo : IRepository<Transaction>
    {
        Task<Transaction> Create(Transaction model);
        Task<Transaction> Update(Transaction model);
    }
}
