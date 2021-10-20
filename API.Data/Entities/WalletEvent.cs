using API.Services.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Data.Entities
{
    public class WalletEvent : EntityBase
    {
        public string EventType { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
