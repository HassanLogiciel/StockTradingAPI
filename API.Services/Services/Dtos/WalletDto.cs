using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Services.Dtos
{
    public class WalletDto
    {
        public string Amount { get; set; }
        public string UserId { get; set; }
        public ICollection<WalletEventDto> WalletEvents { get; set; } = new List<WalletEventDto>();
        public ICollection<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();

    }
}
