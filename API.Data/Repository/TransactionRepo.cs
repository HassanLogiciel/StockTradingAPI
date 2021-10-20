﻿using API.Data.Data;
using API.Data.Entities;
using API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly ApplicationContext _applicationContext;
        public TransactionRepo(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Task<Transaction> Create(Transaction model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _applicationContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetById(string Id)
        {
            return await _applicationContext.FindAsync<Transaction>();
        }

        public Task<Transaction> Update(Transaction model)
        {
            throw new NotImplementedException();
        }
    }
}
