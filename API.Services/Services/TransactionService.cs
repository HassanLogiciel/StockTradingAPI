﻿using API.Common;
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
        private readonly IWalletRepository _walletRepo;
        public TransactionService(IUserRepository userRepo, IUnitOfWork unitOfWork, ITransactionRepo transactionRepo, IWalletRepository walletRepo)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepo;
            _walletRepo = walletRepo;
        }

        public async Task<Response> DepositAsync(DepositVm model)
        {
            var response = new Response();
            if (model != null)
            {
                var wallet = await _walletRepo.GetByIdAsync(model.WalletId);
                if (wallet == null)
                {

                }
                else
                {
                    response.Errors.Add("No wallet found Please contact admin.");
                }
            }
            return response;
        }

        public Task<Response> WithdrawAsync()
        {
            throw new NotImplementedException();
        }
    }
}
