using API.Common;
using API.Data.Interfaces;
using API.Data.Specification;
using API.Services.Services.Dtos;
using API.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<ResponseObject<WalletDto>> GetWalletAsync(string id)
        {
            var response = new ResponseObject<WalletDto>();
            try
            {
                var wallet = await _walletRepository.GetAsync(WalletSpecification.ById(id));
                if (wallet != null)
                {
                    var walletDto = new WalletDto()
                    {
                        Amount = wallet.Amount,
                        UserId = wallet.UserId,
                        WalletEvents = wallet.WalletEvents.Select(c => new WalletEventDto()
                        {
                            Description = c.Description,
                            UserId = c.UserId,
                            EventType = c.EventType
                        }).ToList(),
                        Transactions = wallet.Transactions.Select(c => new TransactionDto()
                        {
                            UserId = c.UserId,
                            Description = c.Description,
                            Amount = c.Amount,
                            Id = c.Id,
                            Status = c.Status.ToString(),
                            Type = c.Type.ToString(),
                            WalletId = c.Wallet.Id
                        }).ToList()
                    };

                    response.RequestedObject = walletDto;
                }
                else
                {
                    response.Errors.Add("Invalid Wallet Id");
                }

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message.ToString());
            }
            return response;
        }
    }
}
