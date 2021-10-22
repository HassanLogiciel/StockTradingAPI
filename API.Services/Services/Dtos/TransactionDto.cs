using API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Services.Dtos
{
    public class TransactionDto
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
        public string WalletId { get; set; }
    }
}
