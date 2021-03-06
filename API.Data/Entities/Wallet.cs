using API.Data.Model;
using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    public class Wallet : EntityBase
    {
        public float Amount { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<WalletEvent> WalletEvents { get; set; } = new List<WalletEvent>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
