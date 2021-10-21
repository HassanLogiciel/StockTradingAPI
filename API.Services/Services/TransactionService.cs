using API.Common;
using API.Data.Interfaces;
using API.Services.Services.Interfaces;
using API.Services.ViewModels;
using System;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepo _transactionRepo;
        public TransactionService(IUserRepository userRepo, IUnitOfWork unitOfWork, ITransactionRepo transactionRepo)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepo;
        }

        public async Task<Response> DepositAsync(DepositVm model)
        {
            var response = new Response();
            if (model != null)
            {
                //var wallet = 
            }
            return response;
        }

        public Task<Response> WithdrawAsync()
        {
            throw new NotImplementedException();
        }
    }
}
