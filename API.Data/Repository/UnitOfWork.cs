using API.Data.Data;
using API.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IdentityContext _identityContext;
        public UnitOfWork(ApplicationContext context, IdentityContext identityContext )
        {
            _context = context;
            _identityContext = identityContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task SaveChanges()
        {
            try
            {
                await _identityContext.SaveChangesAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}
