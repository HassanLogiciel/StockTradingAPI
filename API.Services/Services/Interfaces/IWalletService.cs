using API.Common;
using API.Services.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    public interface IWalletService
    {
        public Task<ResponseObject<WalletDto>> GetWalletAsync(string id);
       
    }
}
