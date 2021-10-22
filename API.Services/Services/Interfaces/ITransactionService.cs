using API.Common;
using API.Data.Entities;
using API.Services.Services.Dtos;
using API.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Response> DepositAsync(DepositVm model);
        public Task<Response> WithdrawAsync(WithdrawVm model);
        public Task<ResponseObject<List<TransactionDto>>> GetUserTransactionsAsync(string id);
        public ResponseObject<List<StatusDto>> GetTransactionsStatuses();
    }
}
