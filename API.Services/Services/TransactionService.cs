using API.Common;
using API.Common.Helpers;
using API.Data.Entities;
using API.Data.Interfaces;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using API.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IWalletRepository _walletRepo;
        private readonly IAppSettingsRepo _appSettingRepo;
        public TransactionService(IUserRepository userRepo, IUnitOfWork unitOfWork, ITransactionRepo transactionRepo, IWalletRepository walletRepo, IAppSettingsRepo appSettingRepo)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _transactionRepo = transactionRepo;
            _walletRepo = walletRepo;
            _appSettingRepo = appSettingRepo;
        }
        public async Task<Response> DepositAsync(DepositVm model)
        {
            var response = new Response();
            try
            {
                if (model != null)
                {
                    var user = await _userRepo.GetByIdAsync(model.UserId);
                    var appSettings = await _appSettingRepo.GetSettings();
                    if (appSettings != null)
                    {
                        if (user != null)
                        {
                            var wallet = await _walletRepo.GetByIdAndUserIdAsync(model.WalletId, model.UserId);
                            if (wallet != null)
                            {
                                if (model.Amount <= appSettings.MaxDeposit && model.Amount > 0.01f)
                                {
                                    var transaction = new Transaction()
                                    {
                                        Type = TransactionType.Deposit,
                                        UserId = wallet.UserId,
                                        Status = Status.Pending,
                                        CreatedBy = user.UserName,
                                        Description = $"Request For amount deposit {model.Amount}",
                                        IsActive = true,
                                        Amount = model.Amount,
                                        Wallet = wallet,
                                    };
                                    await _transactionRepo.Create(transaction);
                                    var result = await _unitOfWork.SaveChanges();
                                    if (!result.IsSuccess)
                                    {
                                        foreach (var item in result.Errors)
                                        {
                                            response.Errors.Add(item);
                                        }
                                    }
                                }
                                else
                                {
                                    response.Errors.Add("Invalid amount. Max amount allowed 5000 and Min amount 0.02");
                                }
                            }
                            else
                            {
                                response.Errors.Add("No wallet found Please contact admin.");
                            }
                        }
                        else
                        {
                            response.Errors.Add("Invalid User.");
                        }
                    }
                    else
                    {
                        response.Errors.Add("Invalid Settings. Please set the currency and max values.");
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
            }

            return response;
        }
        public ResponseObject<List<StatusDto>> GetTransactionsStatuses()
        {
            var response = new ResponseObject<List<StatusDto>>();
            try
            {
                var status = Enum.GetValues(typeof(Status)).Cast<Status>();
                var statusdto = status.Select(c => new StatusDto() { id = ((int)c), Name = c.ToString() }).ToList();
                response.RequestedObject = statusdto;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
            }
            return response;
        }
        public async Task<ResponseObject<List<TransactionDto>>> GetUserTransactionsAsync(string id)
        {
            var response = new ResponseObject<List<TransactionDto>>();
            if (id != null)
            {
                try
                {
                    var transtions = await _transactionRepo.GetByUserId(id);
                    var dto = transtions.Select(c => new TransactionDto()
                    {
                        Id = c.Id,
                        Amount = c.Amount,
                        Description = c.Description,
                        Status = c.Status.ToString(),
                        Type = c.Type.ToString(),
                        UserId = c.UserId,
                        WalletId = c.Wallet.Id,
                    }).ToList();
                    response.RequestedObject = dto;
                }
                catch (Exception ex)
                {
                    response.Errors.Add(ex.ToString());
                }
            }
            return response;
        }
        public async Task<Response> SetTransactionStatus(TransactionStatusVm model)
        {
            var response = new Response();
            try
            {
                if (model != null  && model.Status != Status.Invalid)
                {
                    var user = await _userRepo.GetByIdAsync(model.UserId);
                    if (user != null)
                    {
                        var wallet = await _walletRepo.GetByUserIdAsync(model.UserId);
                        if (wallet != null)
                        {
                            var transaction = await _transactionRepo.GetByIdAsync(model.TransactionId);
                            if (transaction != null)
                            {
                                if (transaction.Status == Status.Pending)
                                {
                                    if (transaction.Wallet.Id == wallet.Id)
                                    {
                                        if (model.Status == Status.Approved)
                                        {
                                            transaction.Status = Status.Approved;
                                            if (transaction.Type == TransactionType.Deposit)
                                            {
                                                var newWalletAmount = wallet.Amount + transaction.Amount;
                                                wallet.Amount = newWalletAmount;
                                            }
                                            else if (transaction.Type == TransactionType.Withdrawal)
                                            {
                                                if (wallet.Amount >= transaction.Amount)
                                                {
                                                    var newWalletAmount = Extenstions.SubtractFloat(wallet.Amount, transaction.Amount);
                                                    wallet.Amount = newWalletAmount;
                                                }
                                                else
                                                {
                                                    response.Errors.Add("Can not Approve Wallet amout is less then transaction amount.");
                                                }
                                            }
                                            else
                                            {
                                                response.Errors.Add("Invalid Transaction Type.");
                                            }
                                        }
                                        else if (model.Status == Status.Rejected)
                                        {
                                            transaction.Status = Status.Rejected;
                                        }
                                        else
                                        {
                                            response.Errors.Add("Invalid Transaction Status.");
                                        }
                                        var result = await _unitOfWork.SaveChanges();
                                        if (!result.IsSuccess)
                                        {
                                            foreach (var item in result.Errors)
                                            {
                                                response.Errors.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        response.Errors.Add("Wallet and Transaction are different.");

                                    }
                                }
                                else
                                {
                                    response.Errors.Add($"Transaction status can not be revert. Transcation status is {transaction.Status.ToString()}");
                                }
                            }
                            else
                            {
                                response.Errors.Add("Invalid transaction id.");
                            }
                        }
                        else
                        {
                            response.Errors.Add("No wallet found. Please contact the admin");
                        }
                    }
                    else
                    {
                        response.Errors.Add("Invalid User.");
                    }
                }
                else
                {
                    response.Errors.Add("Please Enter the valid User Id and Transaction Id.");
                }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message.ToString());
            }
            return response;
        }
        public async Task<Response> WithdrawAsync(WithdrawVm model)
        {
            var response = new Response();
            try
            {
                if (model != null)
                {
                    var user = await _userRepo.GetByIdAsync(model.UserId);
                    var appSettings = await _appSettingRepo.GetSettings();
                    if (appSettings != null)
                    {
                        if (user != null)
                        {
                            var wallet = await _walletRepo.GetByIdAndUserIdAsync(model.WalletId, model.UserId);
                            if (wallet != null)
                            {
                                if (wallet.Amount >= model.Amount)
                                {
                                    if (model.Amount <= appSettings.MaxWithDraw && model.Amount > 0.01f)
                                    {
                                        var updateWalletAmount = Extenstions.SubtractFloat(wallet.Amount, model.Amount);
                                        wallet.Amount = updateWalletAmount;
                                        var transaction = new Transaction()
                                        {
                                            Type = TransactionType.Withdrawal,
                                            UserId = wallet.UserId,
                                            Status = Status.Pending,
                                            CreatedBy = user.UserName,
                                            Description = $"Request For amount withdraw {model.Amount}",
                                            IsActive = true,
                                            Amount = model.Amount,
                                            Wallet = wallet,
                                        };
                                        await _transactionRepo.Create(transaction);
                                        var result = await _unitOfWork.SaveChanges();
                                        if (!result.IsSuccess)
                                        {
                                            foreach (var item in result.Errors)
                                            {
                                                response.Errors.Add(item);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        response.Errors.Add("Invalid amount. Max amount allowed 5000 and Min amount allowed 0.02");
                                    }
                                }
                                else
                                {
                                    response.Errors.Add("Insufficient Balance.");
                                }
                            }
                            else
                            {
                                response.Errors.Add("No wallet found Please contact admin.");
                            }
                        }
                        else
                        {
                            response.Errors.Add("Invalid User.");
                        }
                    }
                    else
                    {
                        response.Errors.Add("Invalid Settings. Please set the currency and max values.");
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.ToString());
            }

            return response;
        }
    }
}
