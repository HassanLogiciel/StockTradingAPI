using API.Data.Model;
using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    public class Wallet : EntityBase
    {
        public double Amount { get; set; }
        public string UserId { get; set; }
        public ICollection<WalletEvent> WalletEvents { get; set; }
    }
}
