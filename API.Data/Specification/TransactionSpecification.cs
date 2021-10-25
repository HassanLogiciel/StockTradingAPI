using API.Data.Entities;
using API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace API.Data.Specification
{
    public class TransactionSpecification : Specification<Transaction>
    {
        //public Expression<Func<Wallet, bool>> Criteria { get;  } 
        public new Expression<Func<Transaction, bool>> Criteria { get; } = c => true;
        public TransactionSpecification(Expression<Func<Transaction, bool>> expression) => Criteria = expression;
        
        public static TransactionSpecification ByUserId(string userId)
        {
            return new TransactionSpecification(c => c.UserId == userId);
        }
    }
}
