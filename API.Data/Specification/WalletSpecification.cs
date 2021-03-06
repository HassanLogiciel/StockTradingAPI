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
        //public Expression<Func<Wallet, bool>> Criteria { get;  } 
        public new Expression<Func<Wallet, bool>> Criteria { get; } = c => true;
        public WalletSpecification(Expression<Func<Wallet, bool>> expression) => Criteria = expression;
        public static WalletSpecification ById(string id)
        {
            return new WalletSpecification(c => c.Id == id);
        }
        public static WalletSpecification ByUserId(string userId)
        {
            return new WalletSpecification(c => c.UserId == userId);
        }

        public static WalletSpecification ByUserIdAndWalletId(string userId, string walletId)
        {
            return new WalletSpecification(c => c.UserId == userId && c.Id == walletId);
        }
    }
}
