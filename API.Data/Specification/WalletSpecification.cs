using API.Data.Entities;
using API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace API.Data.Specification
{
    public class WalletSpecification : Specification<Wallet>
    {
        public WalletSpecification(string userId) : base(b => b.UserId == userId)
        {
        }

        public static void ByUserId()
        {

        }
    }
}
